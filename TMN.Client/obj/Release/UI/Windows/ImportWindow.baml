   M S B A M L   `   `   `  ���� B  >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets !    TMN.UI.Windows.ImportWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -ucclr-namespace:TMN.UserControls     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     -     � Title$   دريافت از فايل�� � 
ResizeMode$ NoResize=�$��Tahoma)�$��RightToLeft=�   lightBlueShadow#��[  $	��220��$	��353��.��]� $��RightToLeft=���. .+��� -    ��Name$ ControlPanel��$��Bottom=�$��
-1,0,-1,-1q�$��32��
����ح�$��1q�._�� .+�"   TMN.UserControls.ImageButton  -     Name$ btnOK��"   TMN.UserControls.ImageButton   Click$ btnOK_Click��$��12,0,0,0q�$
��Left=�$��75��$��23��$��Center=� �� 	IsDefault .    Text$ 
تاييد��   ImageSource %  TMN.Assets.ImageSourceExtension     Path$ 	check.png��  -   $ 	btnCancel��$ انصراف��$��93,0,0,0q�$��23��$��Center=�$
��Left=�$��75��	 �� IsCancel	 .    $ 
cancel.png��� 
 � RowDefinitions
 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��Center=�$
��Wrap=� �� Padding$ 5q�$
��5,15q���لطفا از طريق نرم افزار Net Manager دستور STATTRUNK را اجرا نموده و Log File مربوطه را در اين قسمت انتخاب نماييد.]� $��1��$��10q�.+��� -    ��Name$ BrowseButton��$��Right=�$��3q�$��1q�.��انتخاب فايل�� -    ��Name$ PathTextBox��$��LeftToRight=�$��1q�� -    �Name$ progressBar��$��LeftToRight=�$��2��$��20��$��10q��� -    ��Name$ progressLabel��$��2��$��Right=����� *Margin+ �� ElementName$ progressBar��