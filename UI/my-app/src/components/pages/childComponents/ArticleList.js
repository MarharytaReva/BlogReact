

import React from "react";
import { getUsersArticles } from "../../../UserService";
import Article from "./Article";
class ArticleList extends React.Component{
    constructor(props){
        super(props)
        this.state = {
            list: []
        }
    }
    componentDidMount(){
        getUsersArticles(this.props.userId, 1, (data) =>{
            this.setState({
                list: data.map(x => <Article redir={this.props.redir} key={x.articleId} article={x} variant={this.props.variant}></Article>)
            })
        })
    }
    render(){
        return(
            <div>{this.state.list}</div>
        )
    }
}
export default ArticleList