   M S B A M L   `   `   `  ���� B  >Absolut, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Absolut.ViewAbsolut.View       Absolut.View.ControlExpInc   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Absolut.View     -     � Title$+  %Добавление значений��$	��550��$
��1100�� � WindowStartupLocation$ CenterOwner=� � 	Resources  �� 
TargetType �� Property �� Value   	MainBttns(  "System.Windows.Controls.DatePicker��%Q  '��       '	� U     &	  �     '�� �     '�� H    '  �    �� " ��.���  � ��$ 20(���  � ��$ Center=���  � ��$
 Bold"��� " 	�.���  � ��$ 18(���  � ��$ Center=��� .���  � ��$ 20(���  � ��$	 Top=���  � ��$ 50����  � ��$ 0 0 5 0q��� " ��.���  � ��$ 18(���  � ��$
 Bold"���  � ��$ 3q��� " ��.���  � ��$ 16(���  � ��$ DemiBold"��� "  .���  �@ DisplayDateStart 
01/01/1900��  �@ IsTodayHighlighted$
 True����  �@ FirstDayOfWeek$ Monday=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��.+�	� ���� *TextAddComp+���� *AddValueComp+	� ���� *
TextAddAct+���� *AddValueAct+�� $��1��._��  � ColumnDefinitions �� $
��0.5* ��� $��* ��� $��* �.+��� #��[  $��Добавить счет������ *OpenAddAccount+�� #��[  $#��Добавить доходы��$��1������ *OpenAddIncome+�� #��[  $%��Добавить расходы��$��2������ *OpenAddExpense+�� -    ��Name$ 
AddAccount��$��Stretch=�
���   �$��2q�$��0 60 5 0q�._��� $��3 10 3 0q�.+��� $��Добавить Счет���� $	��0 5q����� *InputAccount+	 �� UpdateSourceTrigger$	 PropertyChanged=��� $��Добавить������ *
AddAccount+�� -   $ 	AddIncome��$��Stretch=�
���   �$��2q�$��1��$��0 60 5 0q�._��  �� $��* ��� $��* �.+��� $
��Auto��$��0��$��5 10 5 0q�.+��� $��
Доход���� $	��0 5q����� *InputIncome+$	 PropertyChanged=��� $	��Top=�$��Center=�$��2��$��0 90 0 0q�$	��350��.+��� $.��(Дата получения дохода��  
   SelectedDate
 �� *
DateIncome+�� $
��Auto��$��1��$��5 10 5 0q�.+��� $��Описание���� $	��0 5q����� *InputIncCom+$	 PropertyChanged=��� $;��5Добавить информацию о доходе��$��2��$��60��$��Bottom=�$��20(�$��DemiBold"�$��5q����� *AddInfoIncome+�� -   $ 
AddExpense��$��Stretch=�
���   �$��2q�$��2��$��0 60 5 0q�._��  �� $��* ��� $��* �.+��� $
��Auto��$��0��$��5 10 5 0q�.+��� $��Расход���� $	��0 5q����� *InputExpense+$	 PropertyChanged=��� $	��Top=�$��Center=�$��2��$��0 95 0 0q�$	��350��.+��� $��Дата расхода��  
 �� *DateExpense+�� $
��Auto��$��1��$��5 10 5 0q�.+��� $��Описание���� $	��0 5q����� *InputExpCom+$	 PropertyChanged=��� $=��7Добавить информацию о расходе��$��2��$��60��$��Bottom=�$��20(�$��DemiBold"�$��5q����� *AddInfoExpense+