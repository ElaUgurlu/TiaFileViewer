   M S B A M L   `   `   `  ����H  DTiaFileViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TiaFileViewerTiaFileViewer  5   	       TiaFileViewer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TiaFileViewer     5   	   $	��600��5   +   $	��800��68   $	��250��6D   $	��250��6S     � Title  6	   �� *MainWindowTitle+ � DataContext 5	      %  TiaFileViewer.BindingProperties  5
   
   .��5      � -   5         �Name$
 grid��5       � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $��* �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ scrollViewer��5      $��0��6-   $��0��6:   $��5��6J   $��Disabled=�6^   $
��Auto=�6�   $��0,0,10,0q�6�   .��5      �� -   5         ��Name$ 
NodesPanel��5       �� Orientation$ 
Horizontal=�61    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      5!      �� 5"   
   -   5        	 ��Name$	 ElementsListView��5"      $��1��6-   $��0��6:   $��5��6J   �� 5#   
   -   5        
 ��Name$
 FileOpenButton��5#      $��3��6)   $��0��66   $��Center=�6F   $��Center=�6c   $��35��6~   $��35��6�    5$        5%      .��5'      �� $��1��6   .+�5(      �� $��Icons/FileOpen.png��6   $��Center=�64   $��Center=�6Q   $��24��6l   $��24��6w   5)      5*      �� 5+   
   $��3��6   $��1��6   $��Center=�6.   $��Center=�6K   $��35��6f   $��35��6q    5,        5-      .��5/      �� .+�50      �� $��Icons/Settings.png��6   $��Center=�64   $��Center=�6Q   $��24��6l   $��24��6w   51      52      �� 53   
   $��3��6   $��2��6   $��Center=�6.   $��Center=�6K   $��35��6f   $��35��6q    54        55      .��57      �� .+�58      �� $��Icons/Compare.png��6   $��Center=�63   $��Center=�6P   $��24��6k   $��24��6v   59      5:      �� 5;   
   $��3��6   $��3��6   $��Center=�6.   $��Center=�6K   $��35��6f   $��35��6q    5<        5=      .��5?      �� .+�5@      �� $��Icons/CsvExport.png��6   $��Center=�65   $��Center=�6R   $��24��6m   $��24��6x   5A      5B      5C      5D      