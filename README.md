# sbwpf

This is a new version, for use with WPF features that were introduced with .NET 9. Specifically, now that the Fluent theme is available to WPF, a new theme / color scheme library was needed.


#sbwpf.themes

This works in conjunction with Fluent. At the Windows settings level, the user has already decided their prefernce for Light or Dark. ThemeManager offers a number of color scheme dictionaries, which consist of named colors and brushes. Each of these is compatible with either Light mode or Dark mode. For example, Colors.Light.Standard will use the Windows-provided brush colors, and Colors.Light.Beige will replace some of those stock brushes with a beige palette. 

Stock WPF controls will automatically apply the selected sbwpf palette. Your custom controls can also reference the brush / color palette. Remember to do so as DynamicResource, not StaticResource, or else the colors might not update when flipping between Light and Dark, or from one palette to another.

sbwpf.themes.ThemeManager exposes properties and events for the convenience of you and your users.

LightPalettes and DarkPalettes
These are ObservableCollection&lt;string&gt; which you can bind to the UI, to present customization options to the user. Example: bind these as ItemsSource in a ComboBox

SelectedLightPalette and SelectedDarkPalette
The current, active palette for each Fluent theme mode. Writeable. UI bindable. Example: bind as SelectedItem in a ComboBox.

LightPaletteChanged and DarkPaletteChanged
These are events which will fire when the user changes the palette, so that you can save and restore the user's palette settings between sessions.

The demo / test-case project serves as an example of how to use sbwpf.themes.

