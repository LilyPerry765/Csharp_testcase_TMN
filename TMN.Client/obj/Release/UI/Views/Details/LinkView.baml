   M S B A M L   `   `   `  ���� M  ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters  U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  TMN    TMN.Views.Details.LinkView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     .����   �� Orientation$  
Horizontal=�.+��� .+��� $��Right=�.��LTG:�� $��Right=� �� Target ��  �� ElementName$ DIUNumericUpDown��.��DIU:�� $��Right=� �� $ SysNumericUpDown��.��Sys:�� $��Right=�.��	آدرس:�� $��Right=�.��DDF:�� $��Right=� �� $ txtDescription��.��توضيحات:�� $
��Auto��.+��� -    ��Name$ txtLTG��$��50�� �� 	MaxLength$ 20��$
��Left=�$��0,2,0,2q����� *LTG+"  Enterprise.Wpf.NumericUpDown  -     Name$ DIUNumericUpDown��$��50��$��0,2,0,2q�   Maximum$ 3X�$
��Left=�   Value ��  �� Path$	 DIU�	 �� Mode$	 TwoWay=�  -   $ SysNumericUpDown��$��50��$ 100000X�$��0,2,0,2q�$
��Left=� �� $	 Sys�$	 TwoWay=��� -   $ 
txtAddress��$	��160��$ 20��$
��Left=�$��0,2,0,2q����� *	Address+�� -   $��25��$	��161��
�������$��LeftToRight=����� *BorderBrush+$ 
txtAddress��
 � DataContext
 �� *DDF+.��`� !   TMN.Converters.DDFConverter   Instance `� 	Converter# Z  `� ConverterParameter$ Bay:{0}, Pos:{1}, No:{2}��.1��� $	 Bay��� $ Position��� $ Number��� -   $ txtDescription�� �� AcceptsReturn . $	��160��$��0,2,0,2q�$
��Left=�" �� VerticalScrollBarVisibility$
 Auto=�$��37������ *Description+