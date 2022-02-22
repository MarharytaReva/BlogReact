import React from "react";
import { getPublishers } from "../../../UserService";
import UserComp from "./UserComp";
class Publishers extends React.Component{
    constructor(props){
        super(props)
        this.state = {
            list: []
        }
    }
    componentDidMount(){
        getPublishers(this.props.userId, 1, (data) => {
            this.setState({
                list: data.map(x => <UserComp user={x} variant="unsub"></UserComp>)
            })
        })
    }
    render(){
        return(
            <div>{this.state.list}</div>
        )
    }
}
export default Publishers