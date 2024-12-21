
# AVL tree

This program allows you to perform operations on an AVL tree.

List of available operations:
1. Create a tree
2. Add an element to the tree
3. Delete the given element (the minimum one on the right)
4. Search for the given element
5. Print the tree
6. Traversal from left to right
7. Releasing the memory

The tree is created from a `.txt` file with data in the following format: Surname First Name Patronymic Hours:Minutes. (For example, `Ponomarev Igor Pavlovich 16:15`).

If desired, the user can add an item that will be **automatically** written to the original database file (2nd menu item).

When using the print function, the pseudo drawing will also be written to the output `.txt` file in addition to output to the terminal.

When traversing the tree, its nodes will also be written to the output file in addition to output to the terminal.
## Run Locally
### Requirements

To build and run the program requires `.NET` ver. **9** and `.NET SDK` for `.NET` ver. **9** and any IDE or code editor that can run `.sln` files (like JetBrains Rider, Microsoft VS, Microsoft VS Code with extension, etc.).

---

Clone the project

```bash
  git clone https://github.com/M4kotoIwasaki/AVL-tree.git
```

Open `AVL_tree.sln` file via your's IDE / Code editor.

Buid an `.exe` file and enjoy the program 🤗.

---
# NOTE!!!

The program is written taking into account the project location on my PC, your project folder location may be different. **ABSOLUTE** paths to `Input.txt` and `Output.txt` files (from the root directory) are used in the program code itself, and for each PC they must be specified separately.