   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls /   )TMN.UI.Windows.InstructionAttachmentsList   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     -ucclr-namespace:TMN.UserControls     $��Tahoma)�  � Title$  
اسناد��$	��300��$	��300��   lightBlueShadow#��[  .��]� $��RightToLeft=�.+��� $	��Top=� �� Orientation$ 
Horizontal=�  ToolBarBrush#��[ .+�"  TMN.UserControls.ImageButton  -     Name$ 	AddButton��"  TMN.UserControls.ImageButton   Click$ AddButton_Click�� � ToolTip$ 
اضافه��$��30��$��30��$��3q�$��3q�  GlassFilter#��[    ImageSource %   TMN.Assets.ImageSourceExtension     Path$ add.png��  -   $ 
ShowButton��$ ShowButton_Click��$ 
نمايش��$��30��$��30��$��3q�$��3q�#��[    $ view.png��  -   $ DeleteButton��$ DeleteButton_Click��$ حذف��$��30��$��30��$��3q�$��3q�#��[    $ 
delete.png���� -    ��Name$ filesListView�� �� SelectedValuePath$ FileName��	 �� View	 �� .P��� 
 �� Width$

 Auto�� �� CellTemplate �� .R���  J� SnapsToDevicePixels . $��32��$��32������ *Image+�� $��نام فايل��$

 Auto�� �� DisplayMemberBinding �� *
FileName+