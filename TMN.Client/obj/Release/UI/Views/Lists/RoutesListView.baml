   M S B A M L   `   `   `  ���� B  >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null1clr-namespace:TMN.Views.ListsTMN.Views.Lists  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters $    TMN.Views.Lists.RoutesListView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     +vclr-namespace:TMN.Views.Lists     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     #   TMN.Views.Lists.ItemsListBase    SearchContent  � $��RightToLeft=�.+�� $
��Auto�� � ColumnDefinitions �� $
��Auto ��� $	��100 �.+��� -    ��Name$ DestCenterLabel��$��0��$��Right=� � ToolTip$ مركز مقصد�� �� Target ��  �� ElementName$ DestCenterComboBox��.��مركز مقصد:�� -    ��Name$ DestCenterComboBox��$��1�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��$��2q�$��Stretch=�� $
��Auto�� �� $
��Auto ��� $	��100 �.+��� -   $ InstructionIDLabel��$��0��$��Right=�$ دستور مداری�� �� $ InstructionIDTextBox��.��دستور مداری:�� -   	 ��Name$	 InstructionIDTextBox��$��1��$��2q�$��Stretch=�� $
��Auto�� �� $
��Auto ��� $	��100 �.+��� -   $ RouteNameLabel��$��0��$��Right=�$ نام مسير�� �� $ RouteNameTextBox��.��نام مسير:�� -   $	 RouteNameTextBox��$��1��$��2q�$��Stretch=�� $
��Auto�� �� $
��Auto ��� $	��100 �.+��� -   $ 	TGNOLabel��$��0��$��Right=�$
 TGNO�� �� $ TGNOTextBox��.��TGNO:�� -   $	 TGNOTextBox��$��1��$��2q�$��Stretch=�
   ListView
 ��  �� View �� .P��� $��سيگنالينگ�� �� Width$
 Auto�� �� CellTemplate �� .R��� $��16����%  TMN.Assets.ImageSourceExtension     Path$ tick.png��y��� *IsSignaling+/  )TMN.Converters.VisibilityBooleanConverter   Instance �� 	Converter# Z �� $��نام مسير��$
 Auto�� �� DisplayMemberBinding �� *	RouteName+�� $��مركز مقصد��$
 Auto�� �� *Dest.Center.Name+�� $
��Dest��$
 Auto�� �� *	Dest.Name+G4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters �� Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     $��پروتكل��$
 Auto�� �� *
Protocol+'  !TMN.Converters.ProtocolsConverter   Instance# Z �� $
��TGNO��$
 Auto�� �� *TGNO+�� $��دستور مداری��$
 Auto�� �� *Instruction.Number+