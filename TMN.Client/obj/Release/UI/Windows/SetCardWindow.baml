   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls "   TMN.UI.Windows.SetCardWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     Dent4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     -ucclr-namespace:TMN.UserControls       � Title$  تخصيص کارت�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterOwner=�$��Tahoma)�$	��182��$	��279��   lightBlueShadow#��[  $��RightToLeft=�.��]� $��RightToLeft=���. .+��� -    ��Name$ ControlPanel��$��Bottom=�$��
-1,0,-1,-1q�$��32��
����ح�$��1q�._�� .+�"  TMN.UserControls.ImageButton  -     Name$ btnOK��"  TMN.UserControls.ImageButton   Click$ btnOK_Click��$��12,0,0,0q�$
��Left=�$��75��$��23��$��Center=� �� 	IsDefault .    Text$ 
تاييد��   ImageSource %   TMN.Assets.ImageSourceExtension  	   Path$	 	check.png��  -   $ 	btnCancel��$ انصراف��$��93,0,0,0q�$��23��$��Center=�$
��Left=�$��75��
 �� IsCancel
 .    $	 
cancel.png��� $	��0,5q� � ColumnDefinitions �� $��* ��� $��3* � � RowDefinitions �� �� �� �� $
��Auto � � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '�� @     �� " ��.���  � ��$ Center=���  � ��$ Right=��� " ��.���  � ��$ Center=���  � ��$
 Left=���  � ��$	 100����  �@��
IsEditable . .+��� $��0��$��0��.��	راک:�� $��0��$��1��.��	شلف:�� $��0��$��2��.��	کارت:�� -    ��Name$ RackComboBox��$��1��$��0�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID���� -   $ ShelfComboBox��$��1��$��1��$
 Name��$ ID���� -   $ CardComboBox��$��1��$��2��$
 Name��$ ID��