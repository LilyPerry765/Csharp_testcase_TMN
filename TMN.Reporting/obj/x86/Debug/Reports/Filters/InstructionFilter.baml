   M S B A M L   `   `   `  ����U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  5       TMN.Reporting+   %TMN.Reports.Filters.InstructionFilter   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      $��RightToLeft=�5      .��5      � $��RightToLeft=�6   .+�5   
   � $
��Auto��6     � ColumnDefinitions  5      �� 5	      $
��Auto �6#   �� 5
      $	��140 �6#   .+�5      �� -   5         ��Name$ DestinationLabel��5      $��0��5      $��Right=�5       � ToolTip$ مقصد��5       �� Target 5      ��  �� ElementName$ DestinationComboBox��.��5   #   	مقصد:6*   �� 5      -   5         ��Name$ DestinationComboBox��5      $��1��5       �� 
IsEditable .  �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ ID��5      $��2q�5      $��Stretch=�5      5      � 5   
   $
��Auto��6     5      �� 5      $
��Auto �6#   �� 5      $	��240 �6#   .+�5      �� -   5        $ IssueDateLabel��5      $��0��5      $��Right=�5       $ تاريخ صدور��5"       5!      �� $ IssueDatePersianDateBox��.��5"   )   تاريخ صدور:66   � 5#      $��1��6     5$      �� 5%      $
��Auto �6'   �� 5&      �� 5'      $
��Auto �6'   �� 5(      .+�5*      �� $��0��6   .��6(   از6,   #   Enterprise.Wpf.PersianDateBox  5+      -   5        	  Name$!	 FromIssueDatePersianDateBox��5,   #   $��1��5+   #   $��2q�5-   #   $��Stretch=�5.   #   �� 5/      $��2��6   .��6(   تا6,     50      -   5        $	 ToIssueDatePersianDateBox��51   #   $��3��50   #   $��2q�52   #   $��Stretch=�53   #   54      55      � 56   
   $
��Auto��6     57      �� 58      $
��Auto �6#   .+�5<      �� -   5        
 ��Name$
 IsDoneCheckBox��5@      $��1��5<      $��Center=�5=       d� IsThreeState . $��انجام شده��5A      $��2q�5B      $��Stretch=�5C       d� 	IsChecked 5>      Z� 5C      5D      5E      5F      