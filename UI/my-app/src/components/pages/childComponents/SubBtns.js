


import { subscribe, unsubscribe } from '../../../SubscribeService'
import { deleteArticle } from '../../../ArticleService'
import CreateArticleComp from './CreateArticleComp'

function SubBtn(props) {
    return (
        <button onClick={() => {
            subscribe({
                subscriberId: localStorage.getItem('id'),
                publisherId: props.publisherId
            })
        }} type="button" className="btn btn-primary float-end markableBtn" style={{ maxHeight: '50px' }}>Subscribe</button>
    )
}
function UnsubBtn(porps) {
    return (
        <button onClick={() => {
            unsubscribe(localStorage.getItem('id'), porps.publisherId)
        }} type="button" className="btn btn-light myLinky float-end" style={{ maxHeight: '50px' }}>Unsubsrcibe</button>
    )
}

function MineBtn(props) {
    return (
        <div className="btn-group float-end" role="group" aria-label="Basic outlined example">
            <button onClick={() => {
                props.redir(<CreateArticleComp articleId={props.articleId}></CreateArticleComp>)
            }} type="button" className="btn btn-outline-warning">Edit</button>
            <button onClick={() =>{
                if (window.confirm('Are you sure you want to delete this post?')) {
                    deleteArticle(props.articleId)
                } 
            }} type="button" className="btn btn-outline-danger">Delete</button>
        </div>
    )
}

export { SubBtn, UnsubBtn, MineBtn }