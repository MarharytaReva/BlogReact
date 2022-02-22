

import { Switch, Route, BrowserRouter as Router } from 'react-router-dom'
import LeftMenu from './LeftMenu'
import Nav from "./Nav"
import UserPage from './pages/UserPage'
import NewsPage from './pages/NewsPage'
import LoginPage from './pages/LoginPage'
import RegistratePage from './pages/RegistratePage'
import { AppContext, defaultContext } from '../Context'
import { useState } from 'react'
import EditUser from './pages/childComponents/EditUser'

function App() {
    const [context, setContext] = useState(defaultContext)
    const setFunc = (param) => {
        setContext({
            isAuthorized: param
        })
    }
    return (
        <div>
            <AppContext.Provider value={context}>
            <Router>
            <Nav func={setFunc}></Nav>
            <div className="container p-xm-0 p-sm-0 p-md-0 m-0 w-100">
                <div className="row h-100">
                    <div className="bg-light col-lg-3 col-xl-3 col-xs-2 col-sm-2 col-md-2 pt-3">
                        <LeftMenu></LeftMenu>
                    </div>

                    <div className="col-lg-9 col-xl-9 col-sm-10 col-xs-10 col-md-10 pt-3" style={{ backgroundColor: 'white' }}>
                        
                            <Switch>
                                <Route exact path='/' component={NewsPage}></Route>
                                <Route path='/userpage/:id' component={UserPage}></Route>
                                <Route path="/edituser" component={EditUser}></Route>
                                <Route path="/login" 
                                   render={(props) => (
                                    <LoginPage {...props} funcSetContext={setFunc} />
                                  )}></Route>
                                <Route path="/registrate" 
                                     render={(props) => (
                                        <RegistratePage {...props} funcSetContext={setFunc} />
                                      )}></Route>
                                <Route component={NewsPage}></Route>
                            </Switch>
                        
                    </div>
                </div>
            </div>
            </Router>
        </AppContext.Provider>
        </div>
    )
}
export default App