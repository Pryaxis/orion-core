---
uid: contributing_guidelines
---

## Contributing Guidelines

Thank you so much for planning on contributing to this project! Before you do so, please read the following guidelines. Most of them should be fairly evident upon browsing the code, but they are replicated here for completeness.

#### Naming Conventions

The following naming conventions should be adhered to:

| Member Type | Naming |
|-------------|--------|
| Types | `PascalCase` |
| Consts | `PascalCase` |
| Public fields | `PascalCase` |
| Private fields | `_camelCase` |
| Properties | `PascalCase` |
| Methods | `PascalCase` |
| Parameters | `camelCase` |
| Local variables | `camelCase` |

#### Coding Conventions

Most coding conventions, such as brace style and spacing, should be enforced by the `.editorconfig` file. IDEs should keep everything consistent.

One important thing to note is that the line limit is 120 characters.

#### Testing

Make sure that any code which is checked in is *100%* covered by tests. All pull requests will have code coverage results at [codecov.io](https://codecov.io/gh/Pryaxis/orion-core).

#### Documentation

All publicly visible members should have XML documentation associated with them. In order to keep consistency, please follow the XML documentation in the codebase.
