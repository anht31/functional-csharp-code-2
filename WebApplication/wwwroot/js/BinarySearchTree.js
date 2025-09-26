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


    BreadthFirstSearchR(queue, list) {
        if (!queue.length) {
            return list;
        }
        const currentNode = queue.shift();
        list.push(currentNode.value);

        if (currentNode.left) {
            queue.push(currentNode.left);
        }
        if (currentNode.right) {
            queue.push(currentNode.right);
        }

        return this.BreadthFirstSearchR(queue, list);
    }

    Validate(queue) {
        let valid = true
        if (!queue.length) return valid

        const { node, min, max } = queue.shift()
        if (node.value <= min || node.value >= max) return false

        node.leff && queue.push({ node: node.left, min: min, max: node.value })
        node.right && queue.push({ node: node.right, min: node.value, max: max})
        return this.Validate(queue)
    }

}

//function traverse(node) {
//    if (!node) return null

//    const value = node.value
//    const left = node?.left && traverse(node?.left) 
//    const right = node?.right && traverse(node?.right)

//    return { value, left, right }
//}

const traverse = node => node && {
    value: node.value,
    left: traverse(node.left),
    right: traverse(node.right)
}

//     9
//  4     20
//1  6  15  70

function traverseIterate2(root) {
    if (!root) return null;

    const result = {};                           // sẽ trả về
    const stack = [{ src: root, dst: result }];

    // DFS using explicit stack
    while (stack.length) {
        const { src, dst } = stack.pop();

        dst.value = src.value;

        // Chuẩn bị rẽ trái & phải
        dst.left = src.left ? {} : null;
        dst.right = src.right ? {} : null;

        // Đẩy right trước để left được xử lý trước (pre-order)
        if (src.right) stack.push({ src: src.right, dst: dst.right });
        if (src.left) stack.push({ src: src.left, dst: dst.left });
    }

    return result;
}

function traverseIterate(node) {

    if (!node)
        return null

    let stack = []
    let targetNode = { value: node.value }
    let tree = targetNode
    let currentNode = node

    let exit = 100

    while (currentNode && --exit > 0) {
        targetNode.value = currentNode?.value
        targetNode.left = null
        targetNode.right = null
        if (currentNode.right) {
            targetNode.right = targetNode.right ?? {}
            stack.push({ source: currentNode.right, target: targetNode.right })
        }

        //currentNode.left && stack.push({ source: currentNode.left, target: targetNode.left })
        if (currentNode.left) {
            currentNode = currentNode.left
            targetNode.left = {}
            targetNode = targetNode.left
        } else if (stack.length > 0) {
            const { source, target } = stack.pop()
            currentNode = source
            targetNode = target
        } else {
            currentNode = null
        }
    }

    return tree
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
        let tree = new BinarySearchTree()
        tree.insert(9)
        tree.insert(4)
        tree.insert(20)
        tree.insert(6)
        tree.insert(70)
        tree.insert(15)
        tree.insert(1)
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
        console.log(traverse(tree.root))
        console.log(traverseIterate(tree.root))
        console.log(tree.lookup(20));
    }
    
    bfs = () => {
        const tree = this.createTree()
        console.log(tree)
        console.log('BFS', tree.BreadthFirstSearchR([tree.root], []))
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
        new Sample().basic()

        // new Sample().validation()

        // new Sample().bfs()
    }
}

export { App }