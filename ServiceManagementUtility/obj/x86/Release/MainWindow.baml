   M S B A M L   `   `   `  ����   Service Management Utility    TMN.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��Tahoma)�  � Icon$  /;component/icon.png�� � Title$$ TMN Service Management Utility�� � WindowStartupLocation$ CenterScreen=�$	��620��$	��700��.��]� .+��� $	��Top=�$��RightToLeft=� �� Orientation$ 
Horizontal=� � 	Resources  �� 
TargetType �� Property �� Value��%,   '��       �� " ��.���  � ��$ 3q�.+��� .��سوييچ:�� -    ��Name$ switchTextBox��$��70���� .��سرويس:�� -   $ serviceTextBox��$��70���� -   	 ��Name$	 refreshButton��$��3q�.��
نمايش�� -   $��Bottom=�
 J� 	AllowDrop
 . .@��� -    ��Name$ versionFinderLabel��$��20��
�������$3��-Drag an assembly file here to get its version��� $��RightToLeft=�.+�&   System.Windows.Controls.DataGrid  -     Name$ servicesDataGrid��   
IsReadOnly .    CanUserAddRows .     AutoGenerateColumns .   � ContextMenu �� -   .@�m� -	    m�Name$ startMenuItem��$��Start��m� -
   $ stopMenuItem��$
��Stop��m� -   $ upgradeMenuItem��$��Upgrade��   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
اتصال��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    
IsReadOnly .    CellTemplate �� .R��� �� -   .�.��� 
���   ����� *Center.IPAddress+0  *System.Windows.Controls.DataGridTextColumn  $ مرکز��$
 Auto  . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Center.Name+  $ 
سوييچ��$
 Auto  .  �� *Center.SwitchType.Name+  $ 
سرويس�� �� *ServiceName+  $ ورژن�� �� *	Version+  $# تاريخ بروزرسانی�� �� *UpgradeTime+  $ مدت بروزرسانی��   SortMemberPath$ UpgradeTime�� �� *UpgradeTimeSpan+  $ 
وضعيت�� �� *Status+