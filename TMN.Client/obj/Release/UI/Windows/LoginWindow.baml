   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets   TMN    TMN.UI.Windows.LoginWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     -     � Title$  ورود به سيستم�� � WindowStartupLocation$ CenterScreen=�   lightBlueShadow#��[   � 
ResizeMode$ NoResize=�$	��211��$��Tahoma)�$��RightToLeft=�$	��353��.��� $��10q� � RowDefinitions �� �� $
��Auto �.+�� $��0��$��0,15,0,0q� �� �� �� ��  � ColumnDefinitions �� $
��Auto �$��71���� $��* ��� $	��100 �.+��� $��2��$��4����%   TMN.Assets.ImageSourceExtension     Path$ 	login.png���� -   $��txtUsername��$��23��$��Center=�$��0��$��1��$��0��G� -   $��txtPassword��$��23��$��Center=�$��1��$��1���� $��28.12��$��2��$��Right=�$��Center=�.��	شيفت:�� -    ��Name$ 
ShiftLabel��$��23�� �� 
IsReadOnly . $��2��$��1��$��Center=�|�.  $
��Left=��� -   $��label2��$��28.12��$��0��$��0��$��Right=�$��Center=�.��نام کاربری:�� -   $��label1��$��1��$��0��$��25��$��Right=�$��Center=�.��کلمه عبور:�� -   $��btnOK��$��1��$
��Left=� �� 	IsDefault . $��75��$	��Top=�$��Stretch=�$��25��.���  �� -   	 ��Name$	 c1��$
��Auto ��� �� ���� *ActualWidth+
 �� ElementName$
 c1��.+��� $��0����  $ 	check.png���� $��1��$��Center=�
ورود�� -	   $��	btnCancel��$��1�� �� IsCancel . $��85,0,0,0q�$
��Left=�$��75��$	��Top=�$��25��$��Stretch=�.���  �� -
   $	 c2��$
��Auto ��� �� ���� *ActualWidth+$
 c2��.+��� $��0����  $ 
cancel.png���� $��1��$��Center=�انصراف