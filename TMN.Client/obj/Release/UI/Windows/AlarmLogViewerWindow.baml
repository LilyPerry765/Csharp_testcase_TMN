   M S B A M L   `   `   `  ���� M  ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  TMN)   #TMN.UI.Windows.AlarmLogViewerWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     :a,clr-namespace:TMN.Assets;assembly=TMN.Common       � WindowStartupLocation$  CenterScreen=�$��Tahoma)� � Title$ 	Alarm Log��$	��469��$	��632��.��]� .+��� $	��Top=�]� $��Bottom=�.+��� -    ��Name$ countTextBlock��$��Right=�$��Center=�$��10q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 1Q�   Opacity$ .5Q���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ clearButton��$��80��$��25��$��5q�.��حذف همه�� -   $ 	AckButton��$��80��$��25��$��5q�.��تایید همه�� -   $ okButton��$��80�� �� 	IsDefault . 	 �� IsCancel	 . $��25��$��5q�.��
بستن�� $	��Top=�$��1q�$ 
Horizontal=�$��RightToLeft=�.+��� $	��3,0q�$��Center=�جستجو:�� -   
 ��Name$
 searchTextBox��$��1q�$	��200��$��LeftToRight=��� -    ��Name$ oldAlarmsCheckbox��$��Center=�$
��10,0q�.��%#مشاهده لاگ های قبلی� .+�&   System.Windows.Controls.DataGrid  -     Name$ logsDataGrid��   
IsReadOnly .    AutoGenerateColumns .  ��.    HeadersVisibility$
 None=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    CellTemplate �� .R��� 
������ $
��0.01q�
������  �� 
IsReadOnly . $
��0,20q����� *Data+ �� Mode$ OneWay=�