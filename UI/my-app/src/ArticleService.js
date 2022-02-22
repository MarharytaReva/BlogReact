function postArticle(article, func){
    let accessToken = localStorage.getItem('token')
   
    fetch(`https://localhost:44340/api/article`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        },
        body: JSON.stringify(article)
    })
    .then(response => {
        console.log("post")
        console.log(response)
        return response.json()
    })
    .then(data =>{
        console.log(data)
        func(data)
    })
}
function deleteArticle(id){
    let accessToken = localStorage.getItem('token')
   
    fetch(`https://localhost:44340/api/article/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
    })
    .then(response => {
        console.log(response)
        return response.json()
    })
    .then(data =>{
        console.log(data)
    })
}
function putArticle(article, func){
    let accessToken = localStorage.getItem('token')
   
    fetch(`https://localhost:44340/api/article`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        },
        body: JSON.stringify(article)
    })
    .then(response => {
        console.log("put")
        console.log(response)
        return response.json()
    })
    .then(data =>{
        console.log(data)
        func(data)
    })
}

function getArticle(id, func) {
    let accessToken = localStorage.getItem('token');
    fetch(`https://localhost:44340/api/article/${id}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken,
        }
    }).then(response => {
        console.log(response)
        return response.json()
    })
        .then(data => {
            console.log(data)
            func(data)
        })
}
export {postArticle, deleteArticle, putArticle, getArticle}