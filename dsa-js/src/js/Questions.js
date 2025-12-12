var reverseString = function (s) {
    if (s.length <= 1) return
    // Suppose a first index, and b last characters in the array s
    let a = 0
    let b = s.length - 1
    while (s[a] && s[b] && a < b) {
        [s[a], s[b]] = [s[b], s[a]]
        a++
        b--
    }
}


// topic: Arrays & Hashing - Hash Map / Pair Summing
/**
 * @param {number[]} nums
 * @param {number} target
 * @return {number[]}
 */
var twoSum = function (nums, target) {
    const seen = new Map()
    for (let i = 0; i < nums.length; i++) {
        const complement = target - nums[i]
        if (seen.has(nums[i]))
            return [seen.get(nums[i]), i]
        seen.set(complement, i)
    }
}

const Brakets = new Map([['(', ')'], ['{', '}'], ['[', ']']])

/**
 * @param {string} s
 * @return {boolean}
 */
var _isValid = function (s) {
    if (s.length == 0) return true
    if (s.length % 2 == 1) return false
    const openBraket = s[0]
    if (Brakets.has(openBraket)) {
        const closeBraket = Brakets.get(openBraket)
        const closeAt = s.indexOf(closeBraket, 1)
        if (closeAt == -1) return false

        const insideBraket = s.slice(1, closeAt)
        const outsideBraket = s.slice(closeAt + 1)
        return isValid(insideBraket) && isValid(outsideBraket)
    }
    return false
}

// Stack / Parentheses Matching
/**
 * @param {string} s
 * @return {boolean}
 */
var isValid = function (s) {
    const stack = []
    const map = { '(': ')', '[': ']', '{': '}' };
    for (const char of s) {
        if (map[char])
            stack.push(map[char]);   // expect close braket
        else if (stack.pop() !== char) 
            return false
    }
    return true
}


/**
 * @param {ListNode} list1
 * @param {ListNode} list2
 * @return {ListNode}
 */
var mergeTwoListsLeetcode = function(list1, list2) {
    const list = new ListNode(0)
    let tail = list
    
    while (list1 && list2) {
        if (list1.val <= list2.val) {
            tail.next = list1
            list1 = list1.next
        } else {
            tail.next = list2
            list2 = list2.next
        }
        tail = tail.next
    }
    tail.next = list1 || list2

    return list.next
};

// Two-Pointers / Merge - K-way Merge
var mergeTwoLists = function(list1, list2) {
    if (list1.length === 0) return list2
    if (list2.length === 0) return list1
    
    const list = []
    let i = 0
    let j = 0

    // list1: [3, 4]
    // list2: [1, 2] -> done first

    while (i < list1.length || j < list2.length) {
        const outOfList1 = i >= list1.length
        if (outOfList1 || list1[i] > list2[j]) {
            // outOfList1 -> fill list 2, with j < list2.length (parent condition)
            // list1[i] > list2[j] -> fill list2[j]
            // outOfList2 -> (outOfList1: false) || false -> else condition 
            list.push(list2[j])
            j++
        } else {
            // List1 remain item && List 2 out of
            // list1[i] < list2[j]
            list.push(list1[i])
            i++
        }
    }
    return list
}

// Sliding Window (Track Min / One-Pass Window)
/**
 * @param {number[]} prices
 * @return {number}
 */
var maxProfit = function(prices) {
    if (prices.length < 2) return 0
    let hold = prices[0]
    let maxValue = 0
    for (let i = 1; i < prices.length; i++) {
        hold = Math.min(hold, prices[i - 1])
        const profit =  prices[i] - hold
        maxValue = Math.max(maxValue, profit)
    }
    return maxValue
}


const isNonAlphanumeric = (c) => {
    const code = c.charCodeAt(0)
    const isDigit = code >= 48 && code <= 57 // 0-9
    const isUpper = code >= 65 && code <= 90 // A-Z
    const isLower = code >= 97 && code <= 122 // a-z
    return !(isDigit || isUpper || isLower)
}

// Two Pointers – Opposite Ends
/**
 * @param {string} s
 * @return {boolean}
 */
var isPalindrome = function(s) {
    let l = 0
    let r = s.length - 1

    while (l < r) {
        while (l < r && isNonAlphanumeric(s[l])) l++
        while (l < r && isNonAlphanumeric(s[r])) r--

        if (l < r) {
            if (s[l].toLowerCase() !== s[r].toLowerCase()) return false
            l++
            r--
        }
    }
    return true
};

// The same LeetCode
function TreeNode(val, left, right) {
    this.val = (val === undefined ? 0 : val);
    this.left = (left === undefined ? null : left);
    this.right = (right === undefined ? null : right);
}

function arrayToTree(arr) {
    if (!arr || arr.length === 0 || arr[0] == null) return null;

    const root = new TreeNode(arr[0]);
    const queue = [root];
    let qi = 0;          // pointer cho queue
    let i = 1;           // pointer cho arr

    while (qi < queue.length && i < arr.length) {
        const node = queue[qi++];

        // left child
        if (i < arr.length) {
            const leftVal = arr[i++];
            if (leftVal != null) {
                node.left = new TreeNode(leftVal);
                queue.push(node.left);
            } else {
                node.left = null;
            }
        }

        // right child
        if (i < arr.length) {
            const rightVal = arr[i++];
            if (rightVal != null) {
                node.right = new TreeNode(rightVal);
                queue.push(node.right);
            } else {
                node.right = null;
            }
        }
    }

    return root;
}

function treeToArray(root) {
    if (!root) return [];

    const res = [];
    const queue = [root];
    let qi = 0;

    while (qi < queue.length) {
        const node = queue[qi++];

        if (node) {
            res.push(node.val);
            queue.push(node.left);
            queue.push(node.right);
        } else {
            res.push(null);
        }
    }

    // Xoá null ở cuối mảng cho gọn (LeetCode thường không hiển thị trailing null)
    while (res.length > 0 && res[res.length - 1] === null) {
        res.pop();
    }

    return res;
}

/**
 * @param {TreeNode} root
 * @return {TreeNode}
 */
var invertTree = function(root) {
    let stack = [root]
    let i = 0
    while (stack[i]) {
        const node = stack[i]
        node.left && stack.push(node.left)
        node.right && stack.push(node.right)
        if (node.left || node.right) {
            const temp = node.left
            node.left = node.right
            node.right = temp
        }
        i++
    }
    return root
}

class Demo {
    reverseString() {
        let s = ["h", "e", "l", "l", "o"]
        reverseString(s)
        console.log(s)
    }

    twoSum() {
        let nums = [2, 7, 11, 15]
        let result = twoSum(nums, 9)
        console.log(result)
    }

    valiParentheses() {
        const s = "({})"
        const result = isValid(s)
        console.log(result)
    }

    mergeTwoLists() {
        const list1 = []
        const list2 = [0]
        const result = mergeTwoLists(list1, list2)
        console.log(result)
    }

    maxProfit() {
        const prices = [7,1,5,3,6,4]
        const profit = maxProfit(prices)
        console.log(profit)
    }

    isPalindrome() {
        const s = "A man, a plan, a canal: Panama"
        const result = isPalindrome(s)
        console.log(result)
    }

    invertTree() {
        const root = [4,2,7,1,3,6,9]
        const tree = arrayToTree(root)
        const result = invertTree(tree)
        const arrayResult = treeToArray(result)
        console.log(arrayResult)
    }
}

class App {
    run() {
        new Demo().invertTree()
    }
}

export { App }
