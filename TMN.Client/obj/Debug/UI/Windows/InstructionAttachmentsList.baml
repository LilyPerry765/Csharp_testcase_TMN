   M S B A M L   `   `   `  ����I  ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  5   	   B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls 5   	   /   )TMN.UI.Windows.InstructionAttachmentsList   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5   	   -ucclr-namespace:TMN.UserControls     5   	   $��Tahoma)�5   	     � Title$  
اسناد��5   	   $	��300��5   	   $	��300��5	   	      lightBlueShadow#��[  5
   	   .��5      ]� $��RightToLeft=�6   .+�5   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�5        ToolBarBrush#��[ 5      .+�5      "  TMN.UserControls.ImageButton  -   5          Name$ 	AddButton��5      "  TMN.UserControls.ImageButton   Click$ AddButton_Click��5       � ToolTip$ 
اضافه��5      $��30��5      $��30��5      $��3q�5      $��3q�5        GlassFilter#��[ 5         ImageSource 5      %   TMN.Assets.ImageSourceExtension     Path$ add.png��5        5      -   5        $ 
ShowButton��5      $ ShowButton_Click��5      $ 
نمايش��5      $��30��5      $��30��5      $��3q�5      $��3q�5      #��[ 5        5        $ view.png��5         5!      -   5        $ DeleteButton��5!      $ DeleteButton_Click��5"      $ حذف��5$      $��30��5%      $��30��5&      $��3q�5'      $��3q�5(      #��[ 5)       5#        $ 
delete.png��5)      5*      �� 5+   
   -   5         ��Name$ filesListView��5+       �� SelectedValuePath$ FileName��5,      	 �� View	 5.      �� 5/      .P�50      �� 
 �� Width$

 Auto��6%    �� CellTemplate 51      �� 52      .R�53   "   ��  J� SnapsToDevicePixels . $��32��55   (   $��32��56   (   ��53   (   �� *Image+56   (   57      59      �� 5:      $��نام فايل��5;   %   $

 Auto��5<   %    �� DisplayMemberBinding 5:   %   �� *
FileName+5<   %   5=      5?      5@      5B      