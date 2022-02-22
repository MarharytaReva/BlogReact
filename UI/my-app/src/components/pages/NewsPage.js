import React from "react";
import { getNews } from '../../UserService'
import { AppContext } from '../../Context'
import Article from "./childComponents/Article";

class NewsPage extends React.Component{
    constructor(props){
        super(props)
        //sessionStorage.removeItem('id')
        //sessionStorage.removeItem('token')
        this.variant = localStorage.getItem('id') ? 'unsub' : 'none'
        this.state = {
            displayList: []
        }
    }
    componentDidMount(){
        let id = this.context.isAuthorized == true ? localStorage.getItem('id') : 0
        getNews(id, 1, (data) =>
        {
            let mapped = data.map(item => <Article key={item.articleId} variant={this.variant} article={item}></Article>)
            this.setState({
                displayList: this.state.displayList.concat(mapped)
            })
        })
    }
    render(){
        return(
            <>
            {this.state.displayList}
            </>
        )
    }
}
NewsPage.contextType = AppContext
export default NewsPage