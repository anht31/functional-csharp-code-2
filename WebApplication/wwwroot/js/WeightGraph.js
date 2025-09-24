
class Neighbor { 
    constructor(node, weight) {
        this.node = node
        this.weight = weight
    }
}

class NodeInfo {
    constructor(dist = Infinity, prev = '') {
        this.dist = dist
        this.prev = prev
    }
}

class WeightGraph {
    /**
     *
     */
    constructor() {
        this.numberOfNodes = 0
        this.adjacentList = {}
        this.table
        this.isCalc = false
    }

    addVertex(node) {
        if (this.adjacentList[node]) return

        this.adjacentList[node] = []
        this.numberOfNodes++
        this.isCalc = false
    }

    addEdge(nodeA, nodeB, weight) {
        if (!this.adjacentList[nodeA] || !this.adjacentList[nodeB]) return

        const neightborA = new Neighbor(nodeA, weight)
        const neightborB = new Neighbor(nodeB, weight)

        this.adjacentList[nodeA].push(neightborB)
        this.adjacentList[nodeB].push(neightborA)
        this.isCalc = false
    }

    showConnections() {
        const nodeNames = Object.keys(this.adjacentList)
        for (let nodeName of nodeNames) {
            let nodeConnections = this.adjacentList[nodeName]
            let connections = ""
            let vertex
            if (!nodeConnections) return

            for (vertex of nodeConnections) {
                connections += `[${vertex.node}, ${vertex.weight}] `
            }
            console.log(nodeName + " --> " + connections)    
        }
    }

    dijkstra(fromNode) {
        if (this.isCalc) return

        const visited = []
        const unVisited = Object.keys(this.adjacentList)
        const table = new Map(
            unVisited.map(k => [k, new NodeInfo()])
        )
        table.set(fromNode, new NodeInfo(0, undefined))

        for (let nodeName of unVisited) {
            const nodeConnections = this.adjacentList[nodeName]
            const shortestDist = table.get(nodeName).dist
            visited.push(nodeName)
            for (let neightbor of nodeConnections) {
                const distance = neightbor.weight + shortestDist
                const shortestNeighbor = table.get(neightbor.node).dist
                distance < shortestNeighbor && table.set(neightbor.node, new NodeInfo(distance, nodeName))
            }
        }

        this.table = table
        this.isCalc = true
    }

    shortestPath(from, to) {
        if (from === to)
            return `Node ${from} and ${to} is the same`

        if (!this.isCalc) 
            this.dijkstra(from)

        let preVertext = this.table.get(to)?.prev
        if (!preVertext) {
            return `Node ${from} never can reach to Node ${to}`
        }

        const nodes = [to]
        while (preVertext) {
            nodes.push(preVertext)
            if (preVertext == from) break

            const vertex = this.table.get(preVertext)
            if (!vertex || !vertex.prev)
                return `Node ${from} never can reach to Node ${to}`
            
            preVertext = vertex.prev
        }
        
        let path = nodes.pop()
        let item
        while (item = nodes.pop()) {
            path += ` -> ${item}`
        }

        return path
    }
}


class App {
    run() {
        console.log("WeightGraph")

        const myGraph = new WeightGraph();
        myGraph.addVertex('a')
        myGraph.addVertex('b')
        myGraph.addVertex('c')
        myGraph.addVertex('d')
        myGraph.addVertex('e')

        myGraph.addEdge('a', 'b', 7)
        myGraph.addEdge('a', 'c', 3)
        myGraph.addEdge('b', 'c', 1)
        myGraph.addEdge('b', 'd', 2)
        myGraph.addEdge('b', 'e', 6)
        myGraph.addEdge('c', 'd', 2)
        myGraph.addEdge('d', 'e', 4)


        // console.log(myGraph)
        // myGraph.showConnections();

        // myGraph.dijkstra('a')
        const result = myGraph.shortestPath('a', 'd')
        console.log(result)
    }
}

export { App }