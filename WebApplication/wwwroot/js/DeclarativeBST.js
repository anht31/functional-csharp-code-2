/* ──────────────────────────  Core data model  ────────────────────────── */
const Leaf = null;                                // dùng null biểu diễn lá

const Node = (value, left = Leaf, right = Leaf) => ({ value, left, right });

/* ──────────────────────────  Helper ‘query’  ────────────────────────── */
const isLeaf = n => n !== Leaf && !n.left && !n.right;
const onlyLeft = n => n.left && !n.right;
const onlyRight = n => !n.left && n.right;

/* Tìm node nhỏ nhất của 1 subtree (pure, không mutate) */
const findMin = node =>
    node.left === Leaf ? node : findMin(node.left);

/* ──────────────────────────  Declarative DELETE  ────────────────────── */
const remove = (node, value) => {
    /* Trường hợp gốc rỗng hoặc không tìm thấy */
    if (node === Leaf) return Leaf;

    /* Chia nhánh theo thứ tự in-order */
    if (value < node.value)
        return Node(node.value, remove(node.left, value), node.right);

    if (value > node.value)
        return Node(node.value, node.left, remove(node.right, value));

    /* ⇒ value === node.value : gặp node cần xoá */

    /* 1. Lá */
    if (isLeaf(node)) return Leaf;

    /* 2. Chỉ có 1 con */
    if (onlyLeft(node)) return node.left;
    if (onlyRight(node)) return node.right;

    /* 3. Hai con */
    const succ = findMin(node.right);            // successor bất biến
    const rightFixed = remove(node.right, succ.value); // xoá succ khỏi cây phải

    /* Trả về node mới, copy giá trị successor, ghép cây con */
    return Node(succ.value, node.left, rightFixed);
};

/* ──────────────────────────  (Các hàm thuần khác)  ───────────────────── */
/* Chèn bất biến để bạn có cây mẫu thử */
const insert = (node, value) => {
    if (node === Leaf) return Node(value);
    return value < node.value
        ? Node(node.value, insert(node.left, value), node.right)
        : Node(node.value, node.left, insert(node.right, value));
};

/* In-order traversal (trả mảng) */
const inorder = node =>
    node === Leaf ? [] : [...inorder(node.left), node.value, ...inorder(node.right)];

/* ──────────────────────────  Demo  ───────────────────────────────────── */

class App {
    run() {
        let bst = Leaf;
        [50, 30, 70, 20, 40, 60, 80, 65].forEach(v => (bst = insert(bst, v)));

        console.log('Before:', inorder(bst));      // [20,30,40,50,60,65,70,80]

        bst = remove(bst, 70);                     // xoá node có 2 con (70)
        bst = remove(bst, 20);                     // xoá lá
        bst = remove(bst, 60);                     // xoá node có 1 con (60→65)

        console.log('After :', inorder(bst));      // [30,40,50,65,80]

    }
}

export { App }