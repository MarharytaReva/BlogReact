function subscribe(subscribeObj){
    let accessToken = localStorage.getItem('token')
   
    fetch(`https://localhost:44340/api/subscribe`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        },
        body: JSON.stringify(subscribeObj)
    })
    .then(response => {
        console.log(response)
        return response.json()
    })
    .then(data =>{
        console.log(data)
    })
}
function unsubscribe(subId, publisherId){
    let accessToken = localStorage.getItem('token')
   
    fetch(`https://localhost:44340/api/subscribe/${subId}/${publisherId}`, {
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
function getSubscripton(subId, publisherId, func) {
    try{
        let accessToken = localStorage.getItem('token');
        fetch(`https://localhost:44340/api/subscribe/${subId}/${publisherId}`, {
            method: 'GET',
            headers: {
                'Authorization': 'bearer ' + accessToken
            }
        }).then(response => {
            console.log(response)
            func(response.status)
        })
    }
    catch(e){
        func(404)
    }
}


export { subscribe, unsubscribe, getSubscripton }