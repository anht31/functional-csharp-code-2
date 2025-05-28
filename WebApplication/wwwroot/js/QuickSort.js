
function QuickSortSimple(array) {

    if (!array || array.length <= 1) return array

    const pivot = PartitionByPivot(array)
    const left = array.slice(0, pivot)
    const right = array.slice(pivot + 1)

    const sortedLeft = QuickSortSimple(left)
    const sortedRight = QuickSortSimple(right)

    return [...sortedLeft, array[pivot], ...sortedRight]
}

function PartitionByPivot(array) {
    let pivot = 0
    let target = array.length - 1

    while (pivot !== target) {

        const isRotatePivot = (array[pivot] < array[target] && pivot > target)
            || (array[pivot] > array[target] && pivot < target)

        //console.log(`${pivot} - ${target}`)

        if (isRotatePivot) {
            let temp = array[pivot]
            array[pivot] = array[target]
            array[target] = temp

            let tempIndex = pivot
            pivot = target
            target = tempIndex
        }

        target += (pivot < target) ? -1 : 1

        //console.log(array)
    }

    return pivot
}

function showArray(array, left, right) {
    let result = []
    for (let i = left; i <= right; i++) {
        result.push(array[i])
    }
    console.log(structuredClone(result))
}


function quickSort(array, left, right) {

    if (left > right) return array

    let pivot = right
    const partitionIndex = partition(array, pivot, left, right)
    quickSort(array, left, partitionIndex - 1)
    quickSort(array, partitionIndex + 1, right)

    return array
}

function partition(array, pivot, left, right) {
    const pivotValue = array[pivot]
    let partitionIndex = left

    console.log(`Pick pivot [${pivot}] -> ${pivotValue}`)
    showArray(array, left, right)

    for (let i = left; i < right; i++) {
        console.log(`\t ${array[i]}`)
        if (array[i] < pivotValue) {
            console.log(`\t\t item < pivot : ${array[i]}`)
            console.log(`\t\t i: [${i}], partitionIndex: [${partitionIndex}]`)
            swap(array, i, partitionIndex);
            partitionIndex++;
        }

        // Nếu có n item > pivotValue, nó chừa ra khoảng trống gọi là A
    }

    console.log(`Swap -> pivot: [${pivot}] with partitionIndex: [${partitionIndex}]`)

    // swap vị trí của pivot để đặt pivot trước A (trước n item đó)

    // [left … partitionIndex - 1]   pivot   [partitionIndex + 1 … right]
    //          (< pivot)                           (≥ pivot)

    swap(array, right, partitionIndex);

    showArray(array, left, right)
    console.log('=======================================================')

    return partitionIndex
}

function swap(array, firstIndex, secondIndex) {
    if (firstIndex === secondIndex)
        return

    console.log('begin swap', firstIndex, secondIndex)
    console.log(array)
    const temp = array[firstIndex]
    array[firstIndex] = array[secondIndex]
    array[secondIndex] = temp
    console.log(array)
    console.log('end swap')
}

class App {
    run() {
        //const numbers = [1, 2, 3, 8, 5, 6, 7, 9, 11, 12, 10]

        const numbers = [9, 1, 2, 4, 5]
        //const numbers = [9, 7, 6, 2, 1];
        //const numbers = [99, 44, 6, 2, 1, 5, 63, 87, 283, 4, 0];


        //const numbers = [2, 3, 1, 5, 4]

        // Select first and last index as 2nd an 3rd parameters
        const answer = quickSort(numbers, 0, numbers.length - 1);
        console.log(answer);
    }
}

export { App }
