   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null!   TMN.Views.Details.EventView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     $��RightToLeft=�$	��302��$	��480��.����   �� Orientation$  
Horizontal=�.+��� .+��� $��Right=�.��تاريخ:�� $��Right=� �� Target ��  �� ElementName$ cmbType��.��نوع عمليات:�� $��Right=� �� $ 
txtComment��.��شرح عمليات:�� .+�#   Enterprise.Wpf.PersianDateBox  -     Name$ LogDate��   DisplayMode$ DateTime=�$	��180��$
��Left=�{��� *
IsLocked+!  TMN.Converters.NotConverter   Instance �� 	Converter# Z    Date �� *Time+3clr-namespace:TMN.UserControlsTMN.UserControls % TMN.UserControls.EntityComboBox  -ucclr-namespace:TMN.UserControls     -     Name$ cmbType��$	��180��	 �� DisplayMemberPath$
	 Name��
 �� SelectedValuePath$
 ID��$��0,2,0,2q�$
��Left=�{��� *
IsLocked+# Z  �� SelectedItem �� *	EventType+�� -    ��Name$ 
txtComment�� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$
 Wrap=�$	��393��$��0,2,0,2q�$
��Left=�$	��224�� �� 
IsReadOnly �� *
IsLocked+���� *	Comment+�� -    ��Name$ chkIsLocked��$y�	Collapsed=�$
��Left=�{��� *
IsLocked+# Z  d� 	IsChecked �� *
IsLocked+.��<:ثبت قطعی و ممانعت از تغييرات آتی