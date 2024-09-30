using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Absolut.Model;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Absolut.ViewModel
{
    class VMReport
    {
        private const string _filePathComp = "Reports\\ReportComp.docx";
        private const string _filePathAct = "Reports\\ReportAct.docx";
        private string _fullfilePathComp;
        private string _fullfilePathAct;

        public event EventHandler<string> Message;

        private RelayCommand? _createRepComp;
        private RelayCommand? _createRepAct;

        public RelayCommand CreateRepComp
        {
            get
            {
                return _createRepComp ??
                    (_createRepComp = new RelayCommand(obj =>
                    {
                        CreateDocComp();
                    },
                    (obj) => PresenterModel.Model != null && PresenterModel.Model.NameCompany != null));
            }
        }

        private void CreateDocComp()
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(_filePathComp, WordprocessingDocumentType.Document))
            {
                // Добавление основных компонентов документа
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Добавление заголовка "Название компании"
                Paragraph titleParagraph = body.AppendChild(CreateStyledParagraph($"{PresenterModel.Model.NameCompany}", "Heading1"));

                // Добавление текста счета
                var f = new NumberFormatInfo { NumberGroupSeparator = " " };
                string accountNumber = PresenterModel.Model.MoneyCompany.ToString("n0", f) + "$";
                Paragraph accountParagraph = body.AppendChild(CreateStyledParagraph($"Счет: {accountNumber}", "Heading2"));

                // Добавление таблицы "История доходов/расходов"
                Table incomeExpenseTable = CreateIncomeExpenseTable();
                body.AppendChild(incomeExpenseTable);

                // Добавление даты создания отчета
                DateTime currentDate = DateTime.Now;
                Paragraph dateParagraph = body.AppendChild(CreateStyledParagraph($"Дата создания отчета: {currentDate}", "Normal"));

                // Сохранение изменений
                wordDocument.Save();

                Message?.Invoke(this, $"Успешно создан отчет по пути : \n{_fullfilePathComp}");
            }
        }

        public RelayCommand CreateRepAct
        {
            get
            {
                return _createRepAct ??
                    (_createRepAct = new RelayCommand(obj =>
                    {
                        CreateDocAct();
                    },
                    (obj) => PresenterModel.Model != null && PresenterModel.Model.CompanyActivity != null));
            }
        }

        private void CreateDocAct()
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(_filePathAct, WordprocessingDocumentType.Document))
            {
                // Добавление основных компонентов документа
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Добавление заголовка "Название компании"
                Paragraph titleParagraph = body.AppendChild(CreateStyledParagraph($"{PresenterModel.Model.CompanyActivity}", "Heading1"));

                // Добавление текста счета
                var f = new NumberFormatInfo { NumberGroupSeparator = " " };
                string accountNumber = PresenterModel.Model.ActivityMoney.ToString("n0", f) + "$";
                Paragraph accountParagraph = body.AppendChild(CreateStyledParagraph($"Счет: {accountNumber}", "Heading2"));

                // Добавление таблицы "История доходов/расходов"
                Table incomeExpenseTable = CreateIncomeExpenseTableAct();
                body.AppendChild(incomeExpenseTable);

                // Добавление даты создания отчета
                DateTime currentDate = DateTime.Now;
                Paragraph dateParagraph = body.AppendChild(CreateStyledParagraph($"Дата создания отчета: {currentDate}", "Normal"));

                // Сохранение изменений
                wordDocument.Save();

                Message?.Invoke(this, $"Успешно создан отчет по пути : \n{_fullfilePathAct}");
            }
        }
        private static Paragraph CreateStyledParagraph(string text, string styleId)
        {
            Paragraph paragraph = new Paragraph();
            Run run = paragraph.AppendChild(new Run());
            Text runText = run.AppendChild(new Text(text));

            // Создание стиля
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId = new ParagraphStyleId() { Val = styleId };
            paragraphProperties.AppendChild(paragraphStyleId);
            paragraph.AppendChild(paragraphProperties);

            return paragraph;
        }

        private static Table CreateIncomeExpenseTable()
        {
            Table table = new Table();

            // Добавление строк и ячеек в таблицу
            TableRow headerRow = new TableRow();
            headerRow.AppendChild(CreateTableCell("Доходы"));
            headerRow.AppendChild(CreateTableCell("Расходы"));
            table.AppendChild(headerRow);

            // Определение максимального количества элементов в обоих словарях
            int maxCount = Math.Max(PresenterModel.Model.HistoryIncome.Count, PresenterModel.Model.HistoryExpense.Count);

            // Заполнение строк таблицы данными из обоих словарей
            for (int i = 0; i < maxCount; i++)
            {
                TableRow row = new TableRow();

                // Добавление данных из словаря доходов
                if (i < PresenterModel.Model.HistoryIncome.Count)
                {
                    row.AppendChild(CreateTableCell(PresenterModel.Model.HistoryIncome.ElementAt(i).Value));
                }
                else
                {
                    row.AppendChild(CreateEmptyTableCell());
                }

                // Добавление данных из словаря расходов
                if (i < PresenterModel.Model.HistoryExpense.Count)
                {
                    row.AppendChild(CreateTableCell(PresenterModel.Model.HistoryExpense.ElementAt(i).Value));
                }
                else
                {
                    row.AppendChild(CreateEmptyTableCell());
                }

                table.AppendChild(row);
            }

            return table;
        }

        private static Table CreateIncomeExpenseTableAct()
        {
            Table table = new Table();

            // Добавление строк и ячеек в таблицу
            TableRow headerRow = new TableRow();
            headerRow.AppendChild(CreateTableCell("Доходы"));
            headerRow.AppendChild(CreateTableCell("Расходы"));
            table.AppendChild(headerRow);

            // Определение максимального количества элементов в обоих словарях
            int maxCount = Math.Max(PresenterModel.Model.HistoryIncAct.Count, PresenterModel.Model.HistoryExpAct.Count);

            // Заполнение строк таблицы данными из обоих словарей
            for (int i = 0; i < maxCount; i++)
            {
                TableRow row = new TableRow();

                // Добавление данных из словаря доходов
                if (i < PresenterModel.Model.HistoryIncAct.Count)
                {
                    row.AppendChild(CreateTableCell(PresenterModel.Model.HistoryIncAct.ElementAt(i).Value));
                }
                else
                {
                    row.AppendChild(CreateEmptyTableCell());
                }

                // Добавление данных из словаря расходов
                if (i < PresenterModel.Model.HistoryExpAct.Count)
                {
                    row.AppendChild(CreateTableCell(PresenterModel.Model.HistoryExpAct.ElementAt(i).Value));
                }
                else
                {
                    row.AppendChild(CreateEmptyTableCell());
                }

                table.AppendChild(row);
            }

            return table;
        }

        // Метод для создания ячейки таблицы с текстом
        private static TableCell CreateTableCell(string text)
        {
            Paragraph paragraph = new Paragraph(new Run(new Text(text)));
            return new TableCell(paragraph);
        }

        // Метод для создания пустой ячейки таблицы
        private static TableCell CreateEmptyTableCell()
        {
            return new TableCell();
        }

        public VMReport()
        {
            _fullfilePathComp = Environment.CurrentDirectory + "\\" + _filePathComp;
            _fullfilePathAct = Environment.CurrentDirectory + "\\" + _filePathAct;
            CheckFolder();
        }

        private void CheckFolder()
        {
            if (!Directory.Exists("Reports"))
            {
                Directory.CreateDirectory("Reports");

            }

            if (!File.Exists(_filePathComp))
            {
                using (FileStream fs = File.Create(_filePathComp))
                {

                }
            }

            if (!File.Exists(_filePathAct))
            {
                using (FileStream fs = File.Create(_filePathAct))
                {

                }
            }
        }
    }
}
