   M S B A M L   `   `   `  ����B  >Absolut, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Absolut.ViewAbsolut.View  5   	        Absolut.View.HistoryExpAct   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Absolut.View     5   	     � Title$C  =История расхода отдельного счета��5   	   $	��450��62   $	��800��6?    � 	Resources 5
       �� 
TargetType �� Property �� Value��%F  '��       '�� �     �� 5   
   " ��6   .�5      ��  � ��$ 16(�6)   �� 5       � ��$ 0 0 5 0q�6'   �� 5       � ��$
 Bold"�6+   5      �� 5   
   " ��6   .�5      ��  � ��$ 18(�6)   �� 5       � ��$ DemiBold"�6+   �� 5       � ��$ 0 0 5 0q�6'   5      5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $��* �6   �� 5"      $
��Auto �6   .+�5%   
   �� $��2��6.   ��6   �� *
TextInfo+6.   �� 5&   
   $%��Выбрать отд. счет��6   $��Right=�6-   $��1��6I   ��5'      �� *SetActivity+�� 5)   
   $��1��6   $��1��6%   $��5q�62   .+�5*      �� $��Расход��6   �� 5+      ��6   �� *	Expense+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5,      $��Дата��6   (  "System.Windows.Controls.DatePicker  5-      	   SelectedDate	 6   �� *DateExpense+�� 5.      $��Комментарий��6   �� 5/      ��6   �� *CommentExpense+$ TwoWay=�$ PropertyChanged=��� 50      $'��!Обновить значения��6   $��0 10 0 0q�61   ��51      �� *
UpdateInfo+52      �� 54   
   $��1��6   
 �� SelectedValuePath$	
 Key��6j   ��6   �� *AllHistoryExpense+ �� SelectedValue 6I   �� *SetKey+��55      �� 56      .R�57      �� .+�58      �� ��6$   �� *Value+59      5:      5<      �� 5>   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��2��6;   $��0 5 0 0q�6O   .+�5?      �� $��Удалить��6   ��5@      �� *DeleteExpense+5A      5C      5E      