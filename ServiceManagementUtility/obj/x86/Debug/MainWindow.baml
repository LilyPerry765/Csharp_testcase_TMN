   M S B A M L   `   `   `  ����  Service Management Utility    TMN.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��Tahoma)�5   	     � Icon$  /;component/icon.png��5   	    � Title$$ TMN Service Management Utility��5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��620��5	   	   $	��700��5
   	   .��5      ]� .+�5   
   �� $	��Top=�6   $��RightToLeft=�5       �� Orientation$ 
Horizontal=�5       � 	Resources 5       �� 
TargetType �� Property �� Value��%U   '��       �� 5      " ��6   .�5      ��  � ��$ 3q�5      5      5      .+�5      �� .��6   سوييچ:6   �� 5      -   5         ��Name$ switchTextBox��5      $��70��5      �� 5      .��6   سرويس:6   �� 5      -   5        $ serviceTextBox��5      $��70��5      �� 5      -   5        	 ��Name$	 refreshButton��5      $��3q�5      .��6    
نمايش6'   5      �� 5   
   -   5        $��Bottom=�5      
 J� 	AllowDrop
 . .@�5"      �� -   5         ��Name$ versionFinderLabel��5$      $��20��5"      
�������$3��-Drag an assembly file here to get its version��5%      5&      � 5'   
   $��RightToLeft=�6   .+�5(      &   System.Windows.Controls.DataGrid  -   5          Name$ servicesDataGrid��5(         
IsReadOnly .    CanUserAddRows .     AutoGenerateColumns .   � ContextMenu 5,      �� 5-      -   5        .@�5.      m� -	   5         m�Name$ startMenuItem��5/   #   $��Start��5.   #   50   #   m� 51      -
   5        $ stopMenuItem��52   #   $
��Stop��51   #   53   #   m� 54      -   5        $ upgradeMenuItem��55   #   $��Upgrade��54   #   56   #   57         Columns 59      4  .System.Windows.Controls.DataGridTemplateColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ 
اتصال��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5;   -      
IsReadOnly .    CellTemplate 5=      �� 5>      .R�5?   "   �� �� 5@   "   -   5        .�5@   E   .�5A   &   �� 
���   ���61   �� *Center.IPAddress+5B   0   5C   #   6/   5D      5F      0  *System.Windows.Controls.DataGridTextColumn  5G      $ مرکز��6)   $
 Auto 5H   )    . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 5J   )   �� *Center.Name+  5K      $ 
سوييچ��6)   $
 Auto 5L   )    .  5N   )   �� *Center.SwitchType.Name+  5O      $ 
سرويس��6)    5P   )   �� *ServiceName+  5Q      $ ورژن��6)    5R   )   �� *	Version+  5S      $# تاريخ بروزرسانی��6)    5T   )   �� *UpgradeTime+  5U      $ مدت بروزرسانی��6)      SortMemberPath$ UpgradeTime��5V   )    5W   )   �� *UpgradeTimeSpan+  5X      $ 
وضعيت��6)    5Y   )   �� *Status+5[      5\      5]      5^      