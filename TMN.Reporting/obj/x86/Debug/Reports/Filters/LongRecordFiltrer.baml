   M S B A M L   `   `   `  ����U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  5       TMN.Reporting*   $TMN.Reports.Filters.LongRecordFilter   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      $��RightToLeft=�5      .��5      � $��RightToLeft=�6   .+�5   
   � $
��Auto��6     � ColumnDefinitions  5      �� 5	      $
��Auto �6#   �� 5
      $	��100 �6#   .+�5      �� -   5         ��Name$ RouteIDLabel��5      $��0��5      $��Right=�5       � ToolTip$ مسير��5       �� Target 5      ��  �� ElementName$ RouteComboBox��.��5   #   	مسير:6*   �� 5      -   5         ��Name$ RouteComboBox��5      $��1��5       �� 
IsEditable .  �� DisplayMemberPath$
 TGNO��5       �� SelectedValuePath$ ID��5      $��2q�5      $��Stretch=�5      5      � 5   
   $
��Auto��6     5      �� 5      $
��Auto �6#   �� 5      $	��140 �6#   .+�5      �� -   5        $ LengthLabel��5      $��0��5      $��Right=�5       $ مدت مکالمه��5"       5!      �� $ LengthNumericUpDown��.��5"   )   مدت مکالمه:66   � 5#      $��1��6     5$      �� 5%      $
��Auto �6'   �� 5&      �� 5'      $
��Auto �6'   �� 5(      .+�5*      �� $��0��6   .��6(   از6,   "   Enterprise.Wpf.NumericUpDown  5+      -   5        	  Name$	 FromLengthNumericUpDown��5,   "   $��1��5+   "   $��2q�5-   "   $��Stretch=�5.   "   �� 5/      $��2��6   .��6(   تا6,     50      -   5        $	 ToLengthNumericUpDown��51   "   $��3��50   "   $��2q�52   "   $��Stretch=�53   "   54      55      � 56   
   $
��Auto��6     57      �� 58      $
��Auto �6#   �� 59      $	��240 �6#   .+�5;      �� -   5        $ 	DateLabel��5;      $��0��5<      $��Right=�5=      $ 
تاريخ��5?       5>      �� $ DatePersianDateBox��.��5?   $   تاريخ:6,   � 5@      $��1��6     5A      �� 5B      $
��Auto �6'   �� 5C      �� 5D      $
��Auto �6'   �� 5E      .+�5G      �� $��0��6   .��6(   از6,   #   Enterprise.Wpf.PersianDateBox  5H      -   5        
  Name$
 FromDatePersianDateBox��5I   #   $��1��5H   #   $��2q�5J   #   $��Stretch=�5K   #   �� 5L      $��2��6   .��6(   تا6,     5M      -   5        $
 ToDatePersianDateBox��5N   #   $��3��5M   #   $��2q�5O   #   $��Stretch=�5P   #   5Q      5R      � 5S   
   $
��Auto��6     5T      �� 5U      $	��100 �6#   �� 5V      $	��100 �6#   .+�5X      �� -	   5        $ ANumberLabel��5X      $��0��5Y      $��Right=�5Z      $ شماره مشترک A��5\       5[      �� $ ANumberTextBox��.��5\   ,   شماره مشترک A:6<   �� 5]      -
   5         ��Name$ ANumberTextBox��5^      $��1��5]      $��2q�5_      $��Stretch=�5`      5a      � 5b   
   $
��Auto��6     5c      �� 5d      $	��100 �6#   �� 5e      $	��100 �6#   .+�5g      �� -   5        $ BNumberLabel��5g      $��0��5h      $��Right=�5i      $ شماره مشترک B��5k       5j      �� $ BNumberTextBox��.��5k   ,   شماره مشترک B:6<   �� 5l      -   5        $ BNumberTextBox��5m      $��1��5l      $��2q�5n      $��Stretch=�5o      5p      5q      5r      