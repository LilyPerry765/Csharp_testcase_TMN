   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=nullclr-namespace:TMNTMN 1clr-namespace:TMN.Views.ListsTMN.Views.Lists '   !TMN.Views.Lists.RackTypesListView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     tclr-namespace:TMN     +vclr-namespace:TMN.Views.Lists     #  TMN.Views.Lists.ItemsListBase    SearchContent  � $��RightToLeft=�.+�� $	��240�� � ColumnDefinitions �� $	��100 ��� $	��140 �.+��� -    ��Name$ SupportingSwitchLabel��$��0��$��Right=� � ToolTip$ 
سوييچ�� �� Target ��  �� ElementName$ SupportingSwitchComboBox��.��سوييچ:�� -    ��Name$ SupportingSwitchComboBox��$��1�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��$��2q�$��Stretch=�� $	��240�� �� $	��100 ��� $	��140 �.+��� -   $ 	NameLabel��$��0��$��Right=�$ نام�� �� $ NameTextBox��.��	نام:�� -   	 ��Name$	 NameTextBox��$��1��$��2q�$��Stretch=�
   ListView
 �� $ �� IsSynchronizedWithCurrentItem$
 TrueY� �� View �� .P��� $��
سوييچ�� �� Width$
 Auto�� �� DisplayMemberBinding �� *SwitchType.Name+�� $��نام رك��$
 Auto�� �� *Name+�� $��
ظرفيت��$
 Auto�� �� *
Capacity+�� $��دابل رک��$
 Auto�� �� CellTemplate �� .R��� $��16����%   TMN.Assets.ImageSourceExtension     Path$ tick.png��y��� *
IsDouble+/  )TMN.Converters.VisibilityBooleanConverter   Instance �� 	Converter# Z 