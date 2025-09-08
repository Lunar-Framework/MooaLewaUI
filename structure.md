临时markdown文件，记录代码结构。



---



### MooaLewaUI.Xaml.Parser

**职责:** 将 XAML 字符串解析成抽象语法树（AST）。

**依赖:** **不依赖于任何外部库。**

**核心组件:**

* **SyntaxTree:** 定义 AST 的节点类型，如 `XamlRootNode`, `XamlElementNode`。

* **Tokens:** 定义 XAML 词法分析中使用的所有 Token。

* **Compiler:** 包含词法分析器（Lexer）和语法分析器（Parser）的实现。



---



### MooaLewaUI.Xaml.Compiler

**职责:** 将 AST 编译并生成 C# 代码。

**依赖:** **仅依赖于 `Xaml.Parser`**。

**核心组件:**

* **Generators:** 具体的代码生成器，如 `ControlGenerator`、`BindingGenerator`。

* **Transforms:** 对 AST 进行转换和优化的逻辑，以生成更高效的代码。



---



### MooaLewaUI.Runtime

**职责:** 跨引擎通用的运行时基础库。

**依赖:** **不依赖于任何特定游戏引擎。**

**核心组件:**

* **Controls:** 定义基础控件类（如 `Control`, `Panel`, `Button`）和 UI 元素。

* **Markup:** 实现 XAML 标记扩展（如 `Binding`, `StaticResource`）的 C# 逻辑。

* **Styling:** 包含样式和模板系统的实现。
