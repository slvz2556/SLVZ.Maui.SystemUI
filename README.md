# SLVZ.MAUI.SystemUI


## That does this package do

- Handle Edge-to-Edge in Android
- Handle Back-Button-Press in Android
- UITheme
- Change status bar and navigation bar color


---

## 1. Edge-to-Edge handler

### Native application

Set the shell directly in `AppShell.cs` inside `AppShell()`:

```csharp
public AppShell()
{
    InitializeComponent();
    Edge2EdgeHandler.SetShell(this);   
}   
```

Done ✅

If you want to build a fullscreen app you can do that 😃

```csharp
public AppShell()
{
    InitializeComponent();
    Edge2EdgeHandler.SetShell(this, false);   
}   
```

Then you can have Statusbar height and Navigationbar height at the same time to handle them by yourself

```csharp
int navHeight = Edge2EdgeHandler.NavbarHeight;   
int statusHeight = Edge2EdgeHandler.StatusbarHeight;
  
```

---

### Hybrid blazor application

Set the page directly in `MainPage.cs` inside `MainPage()`:

```csharp
public MainPage()
{
    InitializeComponent();
    Edge2EdgeHandler.SetPage(this);   
}   
```

Done ✅

You can also use this method in native application and set every page that laoded

If you want to build a fullscreen app you can do that 😃

```csharp
public AppShell()
{
    InitializeComponent();
    Edge2EdgeHandler.SetShell(this, false);   
}   
```

Then you can have Statusbar height and Navigationbar height at the same time to handle them by yourself

```csharp
int navHeight = Edge2EdgeHandler.NavbarHeight;   
int statusHeight = Edge2EdgeHandler.StatusbarHeight;
  
```

---


## 2. Back press handler

In Android 16 and up the `public override void OnBackPressed()` function does not work.

### How to use?

You can use handler wherever you want

```csharp
BackPressHandler.OnBackPressed += (o,e) => 
{
    //Do something
};
```
---

Done ✅


## 3. ThemeHelper

### Get native system default theme

```csharp
var theme = ThemeHelper.SystemTheme;
```
---
This will return an `UITheme` variable



### Set status bar and navigation bar color for Android

They only require a `MAUI Color`

```csharp
ThemeHelper.SetStatusBarColor(color);

ThemeHelper.SetNavigationBarColor(color);
```
---


👨‍💻 **Author:** [SLVZ](https://slvz.dev)