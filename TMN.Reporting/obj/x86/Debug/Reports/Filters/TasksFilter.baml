   M S B A M L   `   `   `  ����U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  5       TMN.Reporting%   TMN.Reports.Filters.TasksFilter   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      .��5      �   � RowDefinitions  5   
   �� 5      $��30 �6    � ColumnDefinitions 5	   
   �� 5
      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ label1��5      $��Right=�5      $��0��5      $��0��5      .��6    
شيفت6&   �� 5   
   -   5         ��Name$ 
ShiftCombo��5       �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ Value��5      $��3q�5      $��70��5      $��0��5      $��1��5      �� 5   
   -   5        $ label4��5      $��Right=�5      $��2��5       $��0��5!      .��6   عنوان تست6(   �� 5"   
   -   5        $ 
TitleCombo��5"      $
 Name��5$      $��70��5%      $��3q�5&      $��0��5'      $��3��5(      �� 5)   
   -   5        $ label2��5,      $��4��5)      $��Right=�5*      $��0��5+      .��5,       از تاريخ6*   #   Enterprise.Wpf.PersianDateBox  5-   
   -   5          Name$ DateFromDateBox��5-      $��3q�5.      $��0��5/      $��5��50      �� 51   
   -   5        $ label3��51      $��Right=�52      $��0��53      $��6��54      .��6    تا تاريخ6*     55   
   -   5        $ DateToDateBox��55      $��3q�56      $��0��57      $��7��58      5:      5;      