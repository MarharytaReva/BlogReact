function authorize(login, password, func){
    let credentials = {
        login: login,
        password: password
    }
    fetch(`https://localhost:44340/api/account/token`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(credentials)
    })
    .then(response => {
        console.log(response)
        if(response.ok === false){
            return false
        } else {
            return response.json()
        }
    })
    .then(data =>{
        if(data != false){
            console.log(data)
            localStorage.setItem('token', data.access_token)
            localStorage.setItem('id', data.username)
            func()
        }
    })
}

function registrate(user, func){
    fetch(`https://localhost:44340/api/account/registrate`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)
    })
    .then(response => {
        console.log(response)
        if(response.ok === false){
            return false
        } else {
            return response.json()
        }
    })
    .then(data =>{
        console.log(data)
        if(data != false){
            localStorage.setItem('token', data.access_token)
            localStorage.setItem('id', data.username)
            func(data)
        }
    })
}

export { authorize, registrate }