   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets   TMN!   TMN.UserControls.CenterTree   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   .���� 
�������$��.5q�._�� -     �Name$  
dockPanel1�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� .+�c� -    c�Name$ toolBar1��$��26��$��0�� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       �� " ��.���  � ��$ 16����  � ��$ 16����  � ��$ Center=���  �@J�SnapsToDevicePixels . .@��� -    ��Name$ btnAddCenter�� � ToolTip$ مركز جديد��.���� ��%   TMN.Assets.ImageSourceExtension  	   Path$	 
center.png���� -   $ btnAdd��$ آيتم جديد��y��� *
Visibility+
 �� ElementName$
 	mnuNewtem��.���� ��  $	 add.png���� -   $ btnEdit��{�.  $ ويرايش��.���� ��  $	 edit.png���� -   $ 	btnDelete��{�.  $ حذف��.���� ��  $	 
delete.png���� -   $ 
btnRefresh��$ به روزرساني��.���� ��  $	 refresh.png��� $��1��$ 
جستجو�� � ColumnDefinitions �� $
��Auto ��� .+��� -	    ��Name$ SearchTextBox��$��20��$��5,0,0,0q�$��-7,2,2,2q�$��1���� $��0��$��20����  $	 
search.png��R� -
    R�Name$ 	treeView1�����	#100000FF$��2�� � ContextMenu �� -    ��Name$ contextMenu��$��RightToLeft=�.@�m� -    m�Name$ 
mnuNewItem��$��آيتم جديد��m� -   $ mnuLinks��$y�	Collapsed=� m� Icon �� $��16��$��16����  $	 link.png��m� -   $��ويرايش�� �� $��16��$��16����  $	 edit.png��m� -   $��حذف�� �� $��16��$��16����  $	 
delete.png���� m� -   $ remoteMenuItem��$��اتصال به مرکز��