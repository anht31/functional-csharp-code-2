class Node {
    constructor(value) {
        this.left = null;
        this.right = null;
        this.value = value;
    }
}

class BinarySearchTree {

    constructor(value) {
        if (value === undefined) {
            this.root = null;
        } else {
            this.root = new Node(value);
        }
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
            return false

        // find removeNode & it's parent
        const { removeNode, parent } = this.$getRemoveNodeAndItsParent(value)

        //console.log(removeNode)
        //console.log(parent)

        if (!removeNode)
            return false

        const direct = removeNode === parent?.left ? 'left' : 'right'
        const isRemoveNodeIsLeaf = !removeNode.left && !removeNode.right
        const isRemoveNodeHasOneChild = !removeNode.left ^ !removeNode.right

        if (isRemoveNodeIsLeaf) {
            if (!parent) {
                this.root = null
                return true
            }

            parent[direct] = null
        }
        else if (isRemoveNodeHasOneChild) {
            if (!parent) {
                this.root = removeNode?.left ?? removeNode?.right
                return true
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

                parentSuccessor.left = successor.right // continue right branch
                successor.left = removeNode.left
                successor.right = removeNode.right

                if (!parent) {
                    this.root = successor
                } else {
                    parent[direct] = successor
                }
            }

        }

        return true
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

        return { removeNode: null, parent: null }
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

    BreadthFirstSearch() {
        let currentNode = this.root
        const list = []
        const queue = []
        queue.push(currentNode)

        while(queue.length > 0) {
            currentNode = queue.shift()
            list.push(currentNode.value)
            currentNode.left && queue.push(currentNode.left)
            currentNode.right && queue.push(currentNode.right)
        }

        return list
    }

    BreadthFirstSearchR(queue, list) {
        if (!queue.length) return list

        const currentNode = queue.shift()
        list.push(currentNode.value)
        currentNode.left && queue.push(currentNode.left)
        currentNode.right && queue.push(currentNode.right)

        return this.BreadthFirstSearchR(queue, list)
    }

    DFSInorder() {
        return traverseInOrder(this.root, [])
    }

    DFSPreorder() {
        return traversePreOrder(this.root, [])
    }

    DFSPostorder() {
        return traversePostOrder(this.root, [])
    }

    Validate(queue) {
        let valid = true
        if (!queue.length) return valid

        const { node, min, max } = queue.shift()
        if (node.value <= min || node.value >= max) return false

        node.left && queue.push({ node: node.left, min: min, max: node.value })
        node.right && queue.push({ node: node.right, min: node.value, max: max})
        return this.Validate(queue)
    }

}

function traverseInOrder(node, list) {
    node.left && traverseInOrder(node.left, list)
    list.push(node.value)
    node.right && traverseInOrder(node.right, list)
    return list
}

function traversePreOrder(node, list) {
    list.push(node.value)
    node.left && traversePreOrder(node.left, list)
    node.right && traversePreOrder(node.right, list)
    return list
}

function traversePostOrder(node, list) {
    node.left && traversePostOrder(node.left, list)
    node.right && traversePostOrder(node.right, list)
    list.push(node.value)
    return list
} 

// DFS - Preorder
function traverseseLegacy(node) {
   if (!node) return null

   const value = node.value
   const left = node?.left && traverseLegacy(node?.left) 
   const right = node?.right && traverseLegacy(node?.right)

   return { value, left, right }
}

// DFS - Preorder
const traverse = node => node && {
    value: node.value,
    left: traverse(node.left),
    right: traverse(node.right)
}

//     9
//  4     20
//1  6  15  70

function DFSPreorderTranverseInterative(root) {
    if (!root) return null

    const result = {}                           // sẽ trả về
    const stack = [{ src: root, dst: result }]

    // DFS using explicit stack
    while (stack.length) {
        const { src, dst } = stack.pop()

        dst.value = src.value

        // Chuẩn bị rẽ trái & phải
        dst.left = src.left && {}
        dst.right = src.right && {}

        // Đẩy right trước để left được xử lý trước (pre-order)
        src.right && stack.push({ src: src.right, dst: dst.right })
        src.left && stack.push({ src: src.left, dst: dst.left })
    }

    return result
}


/**
 * Array -> Tree
 * @param {number[]} data
 * @returns {BinarySearchTree}
 */
function Deserialize(data) {
    if (!data.length || data[0] === null) return null;

    let tree = new BinarySearchTree(data[0]);
    let queue = [tree.root];
    let i = 1;

    while (i < data.length) {
        let current = queue.shift();

        // Left child
        if (data[i] !== null && data[i] !== undefined) {
            current.left = new Node(data[i]);
            queue.push(current.left);
        }
        i++;

        // Right child
        if (i < data.length && data[i] !== null && data[i] !== undefined) {
            current.right = new Node(data[i]);
            queue.push(current.right);
        }
        i++;
    }
    return tree;
}

class Sample {

    createTree = () => {
        //9, 4, 6, 20, 70, 15, 1
        console.log("     9")
        console.log("  4     20")
        console.log("1  6  15  70")
        let tree = new BinarySearchTree()
        tree.insert(9)
        tree.insert(4)
        tree.insert(20)
        tree.insert(6)
        tree.insert(70)
        tree.insert(15)
        tree.insert(1)
        console.log(tree)
        return tree
    }

    createTreeFourLevel = () => {
        console.log(`
                9
              /   \\
            4       20
           / \\     /  \\
          2   6   15   70
         / \\ / \\ / \\   / \\
        1  3 5 7 14 16 69 71`);

        let tree = Deserialize([9, 4, 20, 2, 6, 15, 70, 1, 3, 5, 7, 14, 16, 69, 71])
        console.log(tree)
        return tree
    }

    basic = () => {
        const tree = this.createTree()

        //     9
        //  4     20
        //1  6  15  70
        console.log(tree.root)
        
        tree.remove(9);
        JSON.stringify(traverse(tree.root))
        console.log(tree.lookup(20));
    }
    
    bfs = () => {
        const tree = this.createTree()
        console.log('BFS', tree.BreadthFirstSearch())
        console.log('BFS-R', tree.BreadthFirstSearchR([tree.root], []))
    }

    dfs = () => {
        const tree = this.createTree()
        console.log('DFS Inorder: ', tree.DFSInorder())
        console.log('DFS Preorder: ', tree.DFSPreorder())
        console.log('DFS Postorder: ', tree.DFSPostorder())
        console.log(traverse(tree.root))
        console.log('DFSPreorderTranverseInterative: ', DFSPreorderTranverseInterative(tree.root))
    }

    validation = () => {
        //let tree = Deserialize([2, 1, 3])
        let tree = Deserialize([9, 4, 20, 1, 6, 15, 70])
        //let tree = Deserialize([5, 1, 4, null, null, 3, 6])

        console.log('Validate:', tree.Validate([ {node: tree.root, min: -Infinity, max: Infinity} ]))
    }
}

class App {
    run() {
        // new Sample().basic()

        // new Sample().validation()

        // new Sample().bfs()

        new Sample().dfs()
    }
}

export { App }