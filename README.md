# SBWPF

## A WPF application kit consisting of things I have found useful over time. The four assemblies are as follows:

### Core:
Contains multiple sub-packages, including: 
- A lightweight event logger
- Extension methods for various types
- Collections of helper functions for IO, JSON, and other foci

### Themer:
Gives color-scheme support to both the stock UI and custom controls. Integration into your app is a one-line affair. In addition to the default themes, new themes can be defined using the JSON template.

Themer also features a new Image type, called ThemeSymbol. ThemeSymbols are images for controls with automatic color change based on the active theme. 

### Controls:
- DataGridEx: A small enhancement of the stock WPF DataGrid. It provides automatic saving and restoration of the DataGrid columns: reordering, resizing, and sorting are all captured automatically. All you have to do is implement the IControlSerializer interface and pass in an instance of that class to the DataGridEx instance. Your IControlSerializer implementation will define the details of how "Save" and "Load" work in your application. That is to say, your IControlSerializer might read & write the grid settings to a json file, or a database, or some other medium.

### Test:
In addition to serving as my development test-case, Test also serves as an example of how to integrate and use the various libs and modules that make up the sbwpf package.

---

# To do:

### Controls:
- Finish DataGridEx
- Transfer WebView2 HTML designer/editor to this lib and clean it up for release
