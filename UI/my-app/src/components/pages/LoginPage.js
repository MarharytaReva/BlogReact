import React from "react";
import { Link } from 'react-router-dom'
import { authorize } from '../../AccountService'

class LoginPage extends React.Component{
    constructor(props){
        super(props)
        this.state = {
            login: '',
            password: ''
        }
    }
    onLogin(){
        authorize(this.state.login, this.state.password, () => {
            this.props.funcSetContext(true)
            this.props.history.push('/userpage')
        })
    }
    inputChange(e){
        const { name, value } = e.target
        this.setState(prev =>{
            return{
                ...prev,
                [name]: value
            }
        }, () => console.log(this.state))
    }
    render(){
        return(
            <div className="col-12 px-lg-5 px-xl-5 px-md-3">
                          
                            <div className="form-group mb-2" >
                                <label htmlFor="exampleFormControlInput1">Login</label>
                                <input name="login" value={this.state.login} onChange={(e) => this.inputChange(e)} type="email" className="form-control" id="exampleFormControlInput1" placeholder="name@example.com"/>
                              </div>
                             
                              
                              <div className="form-group mb-2" >
                                <label htmlFor="exampleFormControlInput1">Password</label>
                                <input name="password" value={this.state.password} onChange={(e) => this.inputChange(e)} type="password" className="form-control" id="exampleFormControlInput1" placeholder="Enter your password"/>
                              </div>
                             

                              <div className="d-flex justify-content-center mb-2 mt-3">
                                
                                    <button onClick={() => this.onLogin()} type="button" className="btn markableBtn" style={{minWidth: '100px'}}>Login</button>
                                   
                              </div>
                              <div className="d-flex justify-content-center mb-2">
                                  <Link to="/registrate">
                                    <button type="button" className="btn btn-light myLink">Registration</button>
                                 </Link>
                              </div>
                             
                        
                      </div>
        )
    }
}
export default LoginPage