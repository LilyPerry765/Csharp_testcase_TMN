   M S B A M L   `   `   `  ����I  ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  5      M ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5      B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=nullclr-namespace:TMNTMN 5      1clr-namespace:TMN.Views.ListsTMN.Views.Lists 5      '   !TMN.Views.Lists.RackTypesListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5      Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5      tclr-namespace:TMN     5      +vclr-namespace:TMN.Views.Lists     5      #  TMN.Views.Lists.ItemsListBase    SearchContent  5      � 5	   
   $��RightToLeft=�6   .+�5
      � $	��240��6    � ColumnDefinitions 5      �� 5      $	��100 �6'   �� 5      $	��140 �6'   .+�5      �� -   5         ��Name$ SupportingSwitchLabel��5      $��0��5      $��Right=�5       � ToolTip$ 
سوييچ��5       �� Target 5      ��  �� ElementName$ SupportingSwitchComboBox��.��5   (   سوييچ:60   �� 5      -   5         ��Name$ SupportingSwitchComboBox��5      $��1��5       �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ ID��5      $��2q�5      $��Stretch=�5      5      � 5      $	��240��6    5      �� 5      $	��100 �6'   �� 5      $	��140 �6'   .+�5       �� -   5        $ 	NameLabel��5       $��0��5!      $��Right=�5"      $ نام��5$       5#      �� $ NameTextBox��.��5$   &   	نام:6,   �� 5%      -   5        	 ��Name$	 NameTextBox��5&      $��1��5%      $��2q�5'      $��Stretch=�5(      5)      5*      
   ListView
 5,      �� 5-   
   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6    �� View 5.      �� 5/      .P�50      �� $��
سوييچ��6%    �� Width$
 Auto��51   %    �� DisplayMemberBinding 53   %   �� *SwitchType.Name+�� 54      $��نام رك��6%   $
 Auto��55   %    57   %   �� *Name+�� 58      $��
ظرفيت��6%   $
 Auto��59   %    5;   %   �� *
Capacity+�� 5<      $��دابل رک��6%   $
 Auto��5=   %    �� CellTemplate 5>      �� 5?      .R�5@   "   �� $��16��5A   (   ��5@   (   %   TMN.Assets.ImageSourceExtension     Path$ tick.png��y�5B   (   �� *
IsDouble+/  )TMN.Converters.VisibilityBooleanConverter   Instance �� 	Converter# Z 5C      5E      5F      5H      5J      