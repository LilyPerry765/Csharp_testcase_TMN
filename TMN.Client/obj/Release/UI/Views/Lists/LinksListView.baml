   M S B A M L   `   `   `  ���� B  >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null1clr-namespace:TMN.Views.ListsTMN.Views.Lists  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf -%clr-namespace:TMN;assembly=TMN.CommonTMN #    TMN.Views.Lists.LinksListView   #vclr-namespace:TMN.Views.Lists  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     7enums%clr-namespace:TMN;assembly=TMN.Common     #   TMN.Views.Lists.ItemsListBase    SelectionChanged$$  ItemsListBase_SelectionChanged��   Loaded$ ItemsListView_Loaded��#   TMN.Views.Lists.ItemsListBase   SearchContent � $��RightToLeft=� � 	Resources    
LabelWidth �� Property �� Value  ComboBoxWidth �� 
TargetType��%�   &	        &	      '�� =     �� .���  � ��$ 65 ��� .���  � ��$	 100 ��� " ��.���  � ��$ 16����  � ��$ 16��.+�� $
��Auto�� � ColumnDefinitions �� #��[  �� $	��100 �.+��� -    ��Name$ AddressLabel��$��0��$��Right=�	 � ToolTip$	 Address��
 �� Target
 ��  �� ElementName$ AddressTextBox��.��
Address:�� -    ��Name$ AddressTextBox��$��1��$��2q�$��Stretch=�� $
��Auto�� �� #��[  �� $	��100 �.+��� -   $ RouteIDLabel��$��0��$��Right=�$	 مسير��
 �� $ RouteComboBox��.��TGNO:�� -    ��Name$ RouteComboBox��$��1��$��LeftToRight=� �� 
IsEditable .  �� DisplayMemberPath$
 TGNO�� �� SelectedValuePath$ ID��$��2q�$��Stretch=�� $
��Auto�� �� #��[  �� $	��120 �.+��� $��0��$��Right=�$	 مسير��
 �� $ TypeComboBox��.��وضعيت:�� -   $ TypeComboBox��$��1��$��LeftToRight=�$��2q�$��Stretch=�$
 Enum����  TMN.Assets.EnumDataSource     EnumType$ enums:LinkStatesL����� .R���  �� Orientation$ 
Horizontal=�.+��� $��14��$��14������ *Enum+.  (TMN.Converters.LinkStateToImageConverter   Instance �� 	Converter# Z ��  �� Padding$ 1q�����    ListView ��  �� SelectionMode$ Extended=� � ContextMenu �� .@�m� -    m�Name$ RouteMenuItem��$��کانال ها�� m� Icon �� $��16����%  TMN.Assets.ImageSourceExtension     Path$ 
routes.png��m� -   $ DDFMenuItem��$��اتصال به DDF�� �� $��16����  $ ddf_add.png��m� -   $ AssignToCardMenuItem��$��تخصيص کارت�� �� $��16����  $ card.png�� �� View �� .P���  �� Width$ 25�� �� CellTemplate �� .R��� $��16��$��16������ *State+# Z �� $��کارت��$	 100�� �� DisplayMemberBinding �� *	Card.Name+�� $��Link Address��$
 Auto�� �� *	Address+�� $	��DDF��$
 Auto�� `�   `� ConverterParameter$  {0}-{1}-{2}��!  TMN.Converters.DDFConverter!   Instance" `� 	Converter#" Z! .1��� # �� Path$# DDF.Bay��� $# DDF.Position��� $# 
DDF.Number��� $	��CIC��$
 Auto�� �� *CIC+�� $	��Sys��$
 Auto�� �� *Sys+�� $
��TGNO��$
 Auto�� �� *UniqueRoute.TGNO+�� $��نام مسير��$
 Auto�� �� *UniqueRoute.RouteName+�� $��توضيحات��$	 100�� �� *Description+