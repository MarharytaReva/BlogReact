

function search(name, func) {
    let accessToken = localStorage.getItem('token');
    fetch(`https://localhost:44340/api/user/search/${name}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
    }).then(response => {
        console.log(response)
        return response.json()
    }).then(data => {
        console.log(data)
        func(data)
    })
}

function getUser(id, func) {
    let accessToken = localStorage.getItem('token');
    fetch(`https://localhost:44340/api/user/${id}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
    }).then(response => response.json())
        .then(data => {
            console.log(data)
            func(data)
        })
}

function getUsersArticles(userId, pageNumber, func) {
    let accessToken = localStorage.getItem('token');
    fetch(`https://localhost:44340/api/user/articles/${userId}/${pageNumber}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken,
            'Accept': 'application/json',
            'Content-Type': 'application/json'
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





function getNews(id, pageNumber, func) {
    let accessToken = localStorage.getItem('token');
    fetch(`https://localhost:44340/api/user/news/${id}/${pageNumber}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
    })
        .then(response => {
            console.log(response)
            return response.json()
        })
        .then(data => {
            console.log(data)
            func(data)
        })
}

function getSubscribers(id, pageNumber, func) {
    let accessToken = localStorage.getItem('token');
    fetch(`https://localhost:44340/api/user/subs/${id}/${pageNumber}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
    }).then(response => {
        console.log(response)
        return response.json()
    }).then(data => {
        console.log(data)
        func(data)
    })
}

function getPublishers(id, pageNumber, func) {
    let accessToken = localStorage.getItem('token');
    fetch(`https://localhost:44340/api/user/publishers/${id}/${pageNumber}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken
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

function putUser(user, func){
    let accessToken = localStorage.getItem('token')
   
    fetch(`https://localhost:44340/api/user`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        },
        body: JSON.stringify(user)
    })
    .then(response => {
        console.log(response)
        return response.json()
    })
    .then(data =>{
        console.log(data)
        func(data)
    })
}



export { getUser, search, getUsersArticles, getSubscribers, getPublishers, getNews, putUser }