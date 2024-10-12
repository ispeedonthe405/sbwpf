A WPF application kit consisting of things I have found useful over time.

**Themer**
Gives color-scheme support to both the stock UI and custom controls. Integration and API are simple. In addition to the default themes, it allows the programmer to create custom themes easily.

**Controls**
DataGridEx: A small enhancement of the stock WPF DataGrid. It provides automatic saving and restoration of the DataGrid columns: reordering, resizing, and sorting are all captured automatically. All you have to do is implement the IControlSerializer interface and pass in an instance of that class to the DataGridEx instance.

**Core**
Contains the following:
- Logger: A lightweight event log system, supporting multiple categories (Debug, Info, Notify, Warning, Error, Exception). Easily binds to UX.
- Extensions: Extension methods for List<T>, ObservableCollection<T>, and String. Adds features like AddRange and AddUnique.
- IoUtil: A collection of sanity-saving functions that helps to make the user code thinner and less repetetitive.

