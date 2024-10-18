# SBWPF

## A WPF application kit consisting of things I have found useful over time. The four assemblies are as follows:

### Core:
- Logger: A lightweight event log system, supporting multiple categories (Debug, Info, Notify, Warning, Error, Exception). Easily binds to UX.
- Extensions: Extension methods you might find useful, including some used in this package.
- IoUtil: A collection of sanity-saving functions that helps to make the user code thinner and less repetetitive.

### Themer:
Gives color-scheme support to both the stock UI and custom controls. Integration and API are simple. In addition to the default themes, it allows the programmer to create custom themes easily.

Themer also features a new Image type, called ThemeSymbol. Symbols are images used as control symbols with automatic color selection. Each theme specifies a symbol color. When the active theme is changed, the control symbols automatically recolor themselves to match. This means only one copy of the image library exists; the loaded, active list is recolored upon theme change.

### Controls:
- DataGridEx: A small enhancement of the stock WPF DataGrid. It provides automatic saving and restoration of the DataGrid columns: reordering, resizing, and sorting are all captured automatically. All you have to do is implement the IControlSerializer interface and pass in an instance of that class to the DataGridEx instance. Your IControlSerializer implementation will define the details of how "Save" and "Load" work in your application.

### Test:
In addition to serving as my development test-case, it also serves as an example of how to implement and use the various libs that make up the sbwpf package.

