   M S B A M L   `   `   `  ���� B  >Absolut, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Absolut.ViewAbsolut.View      Absolut.View.MainMenu   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Absolut.View       � Title$  Absolut��$	��450��$	��700�� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� 
TargetType �� Property �� Value��%F   '��       �� " ��.���  � ��$ 16(���  � ��$ Arial)�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+�o� $	��Top=�$��0��$��2��.@�m� $��Счет��.@�m� $��Добавить счет�� m� Command �� *
AddCompany+m� $2��,Добавить отдельный счет�� �� *	AddActive+m� $'��!Добавить значения�� �� *	AddValues+m� $��Создать отчет�� �� *	CreateRep+m� $��Аккаунт��$��Right=�.@�m� $��
Выход�� �� *Exit+m� $��Помощь��.@�m� $��Справочка�� �� *OpenHelpUser+m� $<��6Справочка для Администратора�� �� *OpenHelpAdm+�� $��1��
�������$��2��._��� $��Center=�$��Center=�$��1��$��10q�$��18(�$
��Bold"����� *Model.NameCompany+�� $��Center=�$	��Top=�$	��250��$	��100��
���   �$��2q�
���   � �� CornerRadius$ 20��$��2��$��0 10 0 0q�$��2��._��� $��Stretch=�$��5q�.+��� $��Счет��$��Center=�$	��Top=�$��20(�
��������� $��Center=�$��Center=�$��25(�$
��0 10q�
����������� *FormatAccount+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�� $��3�� � ColumnDefinitions �� $��* ��� $��* � �� $
��Auto ��� $
��Auto �.+��� $6��0Последняя история доходов��$��0��$��Center=�$	��Top=�$
��Bold"�$��0 5 0 0q��� $8��2Последняя история расходов��$��0��$��1��$��Center=�$	��Top=�$
��Bold"�$��0 5 0 0q��� -   $��1��$��2q�$��5q����� *Model.HistoryIncome+���� .R��� .+��� ���� *Value+�� -   $��1��$��1��$��2q�$��5q����� *Model.HistoryExpense+���� .R��� .+��� ���� *Value+�� $��5��$��Center=�$
��Bold"����� *Title+