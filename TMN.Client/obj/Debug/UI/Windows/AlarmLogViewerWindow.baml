   M S B A M L   `   `   `  ����M  ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters  5   	   I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5   	   B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=nullclr-namespace:TMNTMN 5      )   #TMN.UI.Windows.AlarmLogViewerWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5   	   :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5   	   $accessclr-namespace:TMN     5        � WindowStartupLocation$  CenterScreen=�5   	   $��Tahoma)�5   	    � Title$ 	Alarm Log��5	   	   $	��469��5
   	   $	��632��5   	   .��5      ]� .+�5   
   ]� $��Bottom=�6   .+�5      �� -   5         ��Name$ countTextBlock��5      $��Right=�5      $��Center=�5      $��10q�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 1Q�6'      Opacity$ .5Q�5   '   5   &   �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5        $��postPoneButton��5   9   $��تعلیق همه��6   $��25��6-   $��80��6O   6Z   �� 5      -   5         ��Name$ clearButton��5      $��80��5      $��25��5      $��5q�5      .��5   4   حذف همه6=   �� 5      -   5        $ 	AckButton��5       $��80��5      $��25��5      $��5q�5!      .��5"   1   تایید همه6<   �� 5#      -   5        $ okButton��5'      $��80��5#       �� 	IsDefault . 	 �� IsCancel	 . $��25��5&      $��5q�5(      .��5)   0   
بستن66   5*      5+      �� 5,   
   $	��Top=�6   $��1q�5-      $ 
Horizontal=�5.      $��RightToLeft=�5/      .+�50      �� $	��3,0q�6   $��Center=�51      جستجو:6;   �� 52      -   5        
 ��Name$
 searchTextBox��52      $��1q�53      $	��200��55      $��LeftToRight=�56      �� 57      -   5         ��Name$ oldAlarmsCheckbox��58      $��Center=�57      $
��10,0q�5;      .��6%   %#مشاهده لاگ های قبلی6:   �� 5=      -   5        $ postPoneAlarmsCheckbox��5>      $��Center=�5=      $
��10,0q�5A      .��6%   .,مشاهده لاگ های تعلیق شده6?   5B      � 5C   
   .+�5D      &   System.Windows.Controls.DataGrid  -	   5          Name$ logsDataGrid��5E         
IsReadOnly .    AutoGenerateColumns .  ��.    HeadersVisibility$
 None=�5H         Columns 5I      4  .System.Windows.Controls.DataGridTemplateColumn  5J      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6-      CellTemplate 5K      �� 5L      .R�5M   "   �� 
������ $
��0.01q�5N   *   
������  �� 
IsReadOnly . $
��0,20q�5R   *   ��5Q   *   �� *Data+ �� Mode$ OneWay=�5R   *   5S      5U      5W      5X      5Y      5Z      