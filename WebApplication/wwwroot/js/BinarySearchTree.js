class Node {
    constructor(value) {
        this.left = null;
        this.right = null;
        this.value = value;
    }
}

class BinarySearchTree {
    constructor() {
        this.root = null;
    }

    inserts(...items) {
        items.map(x => this.insert(x))
    }

    insert(value) {
        const newNode = new Node(value)
        if (this.root === null) {
            this.root = newNode
            return
        }

        this.$positionAndInsert(this.root, newNode)
    }

    lookup(value) {
        let node = this.root
        if (!node) return null

        while (node) {
            if (node.value === value) return node

            node = value < node.value ? node.left : node.right
        }

        return null
    }

    remove(value) {

        if (!this.root)
            return

        // find removeNode & it's parent
        const { removeNode, parent } = this.$getRemoveNodeAndItsParent(value)

        //console.log(removeNode)
        //console.log(parent)

        if (!removeNode)
            return

        const direct = removeNode === parent?.left ? 'left' : 'right'
        const isRemoveNodeIsLeaf = !removeNode.left && !removeNode.right
        const isRemoveNodeHasOneChild = !removeNode.left ^ !removeNode.right

        if (isRemoveNodeIsLeaf) {
            if (!parent) {
                this.root = null
                return
            }

            parent[direct] = null
        }
        else if (isRemoveNodeHasOneChild) {
            if (!parent) {
                this.root = removeNode?.left ?? removeNode?.right
                return
            }

            parent[direct] = removeNode?.left ?? removeNode?.right
        }
        else // RemoveNode has two child
        {

            // Right Child hasn't Left Child
            if (!removeNode.right?.left) {
                const successor = removeNode.right
                successor.left = removeNode.left

                if (!parent) {
                    this.root = successor
                } else {
                    parent[direct] = successor
                }
            }
            else { // Right Child has Left Child

                // go right, then go left until meet Leaf
                const { successor, parentSuccessor } = this.$getSuccessorAndItsParent(removeNode)

                successor.left = removeNode.left
                successor.right = removeNode.right
                parentSuccessor.left = null

                if (!parent) {
                    this.root = successor
                } else {
                    parent[direct] = successor
                }
            }

        }
    }

    $getRemoveNodeAndItsParent(value) {
        let parent = null
        let currentNode = this.root
        while (currentNode) {
            if (currentNode.value === value) {
                return { removeNode: currentNode, parent }
            }

            parent = currentNode
            currentNode = value < currentNode.value ? currentNode.left : currentNode.right
        }

        return null
    }


    $getSuccessorAndItsParent(removeNode) {
        let parentSuccessor = null
        let successor = null
        let currentNode = removeNode.right

        while (currentNode?.left) {
            parentSuccessor = currentNode
            successor = currentNode.left

            currentNode = currentNode.left
        }

        return { successor, parentSuccessor }
    }

    $positionAndInsert(node, newNode) {
        if (newNode.value < node.value) {
            if (node.left) {
                return this.$positionAndInsert(node.left, newNode)
            }
            node.left = newNode

            //node.left && this.$positionAndInsert(node.left, newNode)
            //node.left ??= newNode
        } else {
            if (node.right) {
                return this.$positionAndInsert(node.right, newNode)
            }
            node.right = newNode
        }
    }
}

function traverse(node) {
    console.log(node?.value)
    const tree = { value: node.value };
    tree.left = node.left === null ? null : traverse(node.left);
    tree.right = node.right === null ? null : traverse(node.right);
    return tree;
}

class App {
    run() {
        //const tree = new BinarySearchTree();
        // 9, 4, 6, 20, 70, 15, 1
        //tree.insert(9);
        //tree.insert(4);
        //tree.insert(6);
        //tree.insert(20);
        //tree.insert(70);
        //tree.insert(15);
        //tree.insert(1);

        //tree.remove(9);
        //console.log(tree.root)
        //JSON.stringify(traverse(tree.root))
        //console.log(traverse(tree.root))
        //console.log(tree.lookup(20));

        //     9
        //  4     20
        //1  6  15  70


        const tree = new BinarySearchTree();
        tree.inserts(1, 6, 5, 9, 8, 10, 7)
        tree.remove(6)

        //tree.insert(30);
        //tree.insert(54);
        //tree.insert(38);
        //tree.insert(55);
        //tree.insert(44);

        //tree.remove(54);
        console.log(tree.root)
        //JSON.stringify(traverse(tree.root))
        //console.log(traverse(tree.root))
        //console.log(tree.lookup(20));
    }
}

export { App }