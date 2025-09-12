# MooaLewaUI

> **Status**: 🚧 Rapid iteration (features and structure subject to frequent changes)

MooaLewaUI is a **C# UI library** designed for **cross-engine development** with a **XAML-style syntax**, inspired by [**WPF**](https://github.com/dotnet/wpf) and [**AvaloniaUI**](https://github.com/AvaloniaUI/Avalonia).
It provides game engine/framework-specific UI components while remaining **engine-agnostic**.

The name of MooaLewaUI originates from **Kamoʻoalewa**, the first human-discovered Earth quasi-satellite—a “small moon” floating in deep space.

This repository contains the core public code of MooaLewaUI and does not depend on any specific game engine.

MooaLewaUI is part of the [Lunar](https://github.com/Bli-AIk/Lunar) ecosystem and relies on `Lunar.Core.Base` to provide its cross-engine interface. It only depends on the base layer for engine-agnostic functionality and does not include the rest of Lunar’s features.
Looking for a modular, engine-independent game core library built on ECS and pure C#? Check out [Lunar](https://github.com/Bli-AIk/Lunar)!

This README is temporary.

## License

MooaLewaUI uses the **same license model as Lunar**:

### Open Source License (LGPL-3.0)

* You may use MooaLewaUI in closed-source projects as long as you do not modify its source code.
* If you modify MooaLewaUI (e.g., add/change core modules), you must release those modifications under the same license (LGPL-3.0).
* Your own game/application code may remain closed-source.

### Commercial License

* If you wish to modify MooaLewaUI and keep those modifications closed-source, or include it in a project where LGPL is not acceptable, you can contact the author for a commercial license.

## References
MooaLewaUI partially references and uses source code from [**AvaloniaUI**](https://github.com/AvaloniaUI/Avalonia), which is licensed under the **MIT License**.

---

> **状态**：🚧 快速迭代中（功能与结构可能频繁变动）

MooaLewaUI 是一个 **C# 跨引擎 UI 库**，采用 **XAML 风格**，灵感源自 [**WPF**](https://github.com/dotnet/wpf) 与 [**AvaloniaUI**](https://github.com/AvaloniaUI/Avalonia)。
它提供面向游戏引擎/框架的 UI 组件，同时保持 **引擎无关**。

MooaLewaUI 这一名称源自 **Kamoʻoalewa**，人类发现的第一颗地球准卫星——漂浮在深空中的“小月亮”。

此仓库仅包含 MooaLewaUI 的核心公共代码，不依赖任何特定游戏引擎。

MooaLewaUI 是 [Lunar](https://github.com/Bli-AIk/Lunar) 项目的一部分，它借助 `Lunar.Core.Base` 实现跨引擎接口，仅依赖核心基础层提供的引擎无关功能，不包含 Lunar 的其他模块。

如果你想要一个 **基于 ECS、纯 C#、引擎无关** 的模块化游戏核心库，不妨看看 [Lunar](https://github.com/Bli-AIk/Lunar)！

此 Readme 是临时的。

## 许可证
MooaLewaUI 采用双重许可模式：

### 开源许可证（LGPL-3.0）
- 只要您不修改 MooaLewaUI 的源代码，就可以在闭源项目中使用 MooaLewaUI。
- 如果您修改了 MooaLewaUI（例如添加 / 更改核心模块），您必须在相同的许可证（LGPL-3.0）下发布这些修改。
- 您自己的游戏 / 应用程序代码可以保持闭源。

### 商业许可
如果您希望修改 MooaLewaUI 并保持这些修改闭源，或在不接受 LGPL 的项目中包含 MooaLewaUI，您可以联系我以获取商业许可。

## 引用说明
MooaLewaUI 部分参考并使用了 [**AvaloniaUI**](https://github.com/AvaloniaUI/Avalonia) 的源码，Avalonia 以 **MIT 许可证** 发布。
