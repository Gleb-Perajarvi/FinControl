   M S B A M L   `   `   `  ����B  >Absolut, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Absolut.ViewAbsolut.View  5   	       Absolut.View.Activities   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Absolut.View     5   	   -   5          � Title$#  Отдельные счета��5   	   $	��450��6!   $	��800��6.    � WindowStartupLocation$ CenterOwner=�5	   	    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       '�� H     �� 5   
   " ��6   .�5      ��  � ��$ 16(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 18(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 0 3 10 0q�6'   5      5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��0.7* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5       $��0.14* �6   .+�5#   
   �� $2��,Выберите отдельный счет��6   $��Center=�63   $
��Bold"�6P   �� 5%   
   $��0��6   $��1��6"   ��6/   �� *Currentmodel.Activities+ �� SelectedItem 6_   �� *SelectedItem+��5&      �� 5'      .R�5(      �� $��2q�6!   .+�5)      �� ��6$   �� *.+5*      5+      5-      �� 5/   
   $��5q�6   $��1��6    $��1��60   .+�50      �� $6��0Название отдельного счета��6   $��1��69   $��1��6I   �� 51      $��16(�6   ��6$   �� *	InputText+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�52      �� 54   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��2��6;   .+�55      �� $'��!Добавить отд. счет��6   ��56      �� *AddActivity+�� 57      $%��Удалить отд. счет��6   ��58      �� *RemoveActivity+��59      �� *SelectedItem+�� 5:      $%��Выбрать отд. счет��6   ��5;      �� *SetActivity+��5<      �� *SelectedItem+5=      5?      5@      