   M S B A M L   `   `   `  ����B  >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  5   	       TMN.RolesWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -ucclr-namespace:TMN.UserControls     5   	   -   5          � Title$  مدیریت نقش ها��5   	   $	��500��5   	   $	��480��5   	   $��Tahoma)�5   	      lightBlueShadow#��[  5	   	    � 	Resources 5        TreeViewItemStyle �� 
TargetType �� Property �� Value �� Mode-  'System.Windows.Input.KeyboardNavigation  CheckBoxItemTemplate �� ItemsSource �� Orientation d� 	IsChecked�� 5   
   %�  &	       &	 �     �� 5      " Q�6.   .�5      ��  �@Q�
IsExpanded . 6/   �� 5       �@Q�
IsSelected 6/   �� *IsInitiallySelected+$ OneTime=��� 5       �@ AcceptsReturn . 6E   5      �� 5       6D   �� *
Children+$ OneTime=�.R�5      �� $ 
Horizontal=�6   .+�5      �� |�.  $��Center=�6Q    61   �� *	IsChecked+6Q   �� 5      $	��2,0q�6N   ��6'   �� *Name+$ OneTime=�6N   5      5      5      .��5      ]� $��RightToLeft=�6   .+�5      �� $��3q�6   $ 
Horizontal=�6$   $	��Top=�6=   .+�5      �� $��نقش ها :��6   %  TMN.UserControls.EntityComboBox  5!      -   5        	  Name$	 cmbRole��5!       %   TMN.UserControls.EntityComboBox
   SelectionChanged$
 cmbRole_SelectionChanged��6=   $	��150��61    �� DisplayMemberPath$
 Name��6i    �� SelectedValuePath$ ID��6�   $��Center=�6�   5$      �� 5&   
   $
��Left=�6   $��30��64   $ 
Horizontal=�6A   $��Bottom=�6Z   .+�5'      �� -   5        $��btnOK��5'   ;   $��3q�6   $��70��6    $��
تایید��6+   6H   �� 5(      -   5        $��btnDeleteRole��5(   ,   $��3q�6   $��70��6    $��حذف نقش��6A   6S   �� 5)      -   5        $��btnSelectAll��5)   @   $��3q�6   $��70��6    $��انتخاب همه��6+   6T   �� 5*      -   5        $��	btnCancel��5*   <   $��3q�6   $��70��6    $��انصراف��6+   6M   5+      R� 5-   
   -   5        $��treeView��5-       $��3q�6   #��[ 61   #��[ 6i   50      52      