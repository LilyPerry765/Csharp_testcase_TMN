   M S B A M L   `   `   `  ����B  >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null1clr-namespace:TMN.Views.ListsTMN.Views.Lists  5      I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5      M ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5      $    TMN.Views.Lists.RoutesListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      +vclr-namespace:TMN.Views.Lists     5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5      Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	      #   TMN.Views.Lists.ItemsListBase    SearchContent  5      � 5   
   $��RightToLeft=�6   .+�5      � $
��Auto��6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $	��100 �6'   .+�5      �� -   5         ��Name$ DestCenterLabel��5      $��0��5      $��Right=�5       � ToolTip$ مركز مقصد��5       �� Target 5      ��  �� ElementName$ DestCenterComboBox��.��5   ,   مركز مقصد:68   �� 5      -   5         ��Name$ DestCenterComboBox��5      $��1��5       �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ ID��5      $��2q�5      $��Stretch=�5      5      � 5      $
��Auto��6    5       �� 5!      $
��Auto �6'   �� 5"      $	��100 �6'   .+�5$      �� -   5        $ InstructionIDLabel��5$      $��0��5%      $��Right=�5&      $ دستور مداری��5(       5'      �� $ InstructionIDTextBox��.��5(   .   دستور مداری:6<   �� 5)      -   5        	 ��Name$	 InstructionIDTextBox��5*      $��1��5)      $��2q�5+      $��Stretch=�5,      5-      � 5.      $
��Auto��6    5/      �� 50      $
��Auto �6'   �� 51      $	��100 �6'   .+�53      �� -   5        $ RouteNameLabel��53      $��0��54      $��Right=�55      $ نام مسير��57       56      �� $ RouteNameTextBox��.��57   +   نام مسير:66   �� 58      -   5        $	 RouteNameTextBox��59      $��1��58      $��2q�5:      $��Stretch=�5;      5<      � 5=      $
��Auto��6    5>      �� 5?      $
��Auto �6'   �� 5@      $	��100 �6'   .+�5B      �� -   5        $ 	TGNOLabel��5B      $��0��5C      $��Right=�5D      $
 TGNO��5F       5E      �� $ TGNOTextBox��.��5F   '   TGNO:6.   �� 5G      -   5        $	 TGNOTextBox��5H      $��1��5G      $��2q�5I      $��Stretch=�5J      5K      5L      
   ListView
 5N      �� 5O   
    �� View 5P      �� 5Q      .P�5R      �� $��سيگنالينگ��6%    �� Width$
 Auto��5S   %    �� CellTemplate 5T      �� 5U      .R�5V   "   �� $��16��5W   (   ��5V   (   %  TMN.Assets.ImageSourceExtension     Path$ tick.png��y�5X   (   �� *IsSignaling+/  )TMN.Converters.VisibilityBooleanConverter   Instance �� 	Converter# Z 5Y      5[      �� 5\      $��نام مسير��6%   $
 Auto��5^   %    �� DisplayMemberBinding 5]   %   �� *	RouteName+5^   %   �� 5`      $��مركز مقصد��6%   $
 Auto��5b   %    5a   %   �� *Dest.Center.Name+5b   %   �� 5c      $
��Dest��6%   $
 Auto��5e   %    5d   %   �� *	Dest.Name+5e   %   G4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5g   %   �� 5f      Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5g   %   $��پروتكل��5f   %   $
 Auto��5i   %    5h   %   �� *
Protocol+'  !TMN.Converters.ProtocolsConverter   Instance# Z 5i   %   �� 5j      $
��TGNO��6%   $
 Auto��5l   %    5k   %   �� *TGNO+5l   %   �� 5m      $��دستور مداری��6%   $
 Auto��5o   %    5n   %   �� *Instruction.Number+5o   %   5p      5r      5t      