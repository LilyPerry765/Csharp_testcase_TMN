   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls )   #TMN.UI.Windows.ChangePasswordWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     -ucclr-namespace:TMN.UserControls        lightBlueShadow#��[  $��Tahoma)�  � Title$   تغيير رمز عبور�� � 
ResizeMode$ NoResize=�$	��206��$	��260��.��]� $��RightToLeft=���. .+��� -    ��Name$ ControlPanel��$��Bottom=�$��
-1,0,-1,-1q�$��32��
����ح�#��[  $��1q�._�� .+�"  TMN.UserControls.ImageButton  -     Name$ btnOK��"  TMN.UserControls.ImageButton   Click$ btnOK_Click��$��Center=�$
��Left=�$��12,0,0,0q�$��75��$��23�� �� 	IsDefault .    Text$ 
تاييد��   ImageSource %   TMN.Assets.ImageSourceExtension     Path$ 	check.png��  -   $ 	btnCancel��$��93,0,0,0q�$��23��$��Center=�$
��Left=�$��75��	 �� IsCancel	 . $ انصراف��   $ 
cancel.png���� $
��Auto=�|�.  .��� $��25q�
 � ColumnDefinitions
 �� $
��Auto ���  � RowDefinitions �� �� ��  � 	Resources  �� 
TargetType �� Property �� Value��%-   'G�       �� " G�.���  � ��$ 21��.+��� $��0��$��0��.��رمز فعلی:�� $��0��$��1��.��رمز جديد:�� $��0��$��2��.��تکرار رمز:G� -    G�Name$ currentPasswordBox��$��1��$��0��$��Center=�G� -   $ newPasswordBox��$��1��$��1��$��Center=�G� -   $ repeatPasswordBox��$��1��$��2��$��Center=�