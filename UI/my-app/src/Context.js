import React from "react";

let defaultContext = {
    isAuthorized: localStorage.getItem('token') ? true : false
    //isAuthorized: false
}

const AppContext = React.createContext(defaultContext)

export {AppContext, defaultContext}