   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters     TMN.Views.Details.TaskView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     -ucclr-namespace:TMN.UserControls     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     $	��300��$	��456��.����   �� Orientation$  
Horizontal=�.+��� .+��� $��Right=� �� Target ��  �� ElementName$ cmbType��.��عنوان:�� $��Right=�.��	مسير:�� $��Right=�.��تعداد کانال:�� $��Right=� �� $ 	chkIsDone��.��انجام شد:�� $��Right=� �� $ txtFinishDate��.��زمان انجام:�� $��Right=� �� $ 
txtComment��.��توضيحات:�� .+�3clr-namespace:TMN.UserControlsTMN.UserControls % TMN.UserControls.EntityComboBox  -ucclr-namespace:TMN.UserControls     -     Name$ cmbType��$	��140�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��$��0,2,0,2q�$
��Left=� �� SelectedItem �� *
TaskType+  -   $ RouteComboBox�� �� 
IsEditable . $	��140��$
 TGNO��$ ID��$��0,2,0,2q�$
��Left=� �� *Route+"   Enterprise.Wpf.NumericUpDown  -     Name$ ChannelCountNumericUpDown��$��0,2,0,2q�$
��Left=�	   Value	 �� 
 �� Path$
 ChannelCount� �� Mode$ TwoWay=��� -    ��Name$ 	chkIsDone��$	��140��$��4,7,0,7q�$
��Left=� d� 	IsChecked �� *IsDone+#   Enterprise.Wpf.PersianDateBox  -     Name$ txtFinishDate��   DisplayMode$ DateTime=�$��0,2,0,2q�$
��Left=�   ShowCheckBox .     Date �� *
FinishDate+�� -    ��Name$ 
txtComment��" �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$
 Wrap=�$	��391��$��0,2,0,2q�$
��Left=�$	��160�� �� AcceptsReturn . ���� *	Comment+