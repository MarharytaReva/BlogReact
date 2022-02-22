
import { SubBtn, UnsubBtn, MineBtn } from './SubBtns'
import { getSubscripton } from '../../../SubscribeService'
import { useState } from 'react';

function UserComp(props) {
    const [re, setRe] = useState('')
    let btn
    switch (props.variant) {
        case 'sub':
            btn = <SubBtn publisherId={props.user.userId} />
            break;
        case 'unsub':
            btn = <UnsubBtn publisherId={props.user.userId} />
            break;
        case 'none':
            btn = ''
            break;
        case 'async':
            getSubscripton(localStorage.getItem('id'), props.user.userId, (status) => {
                if (status == 200) {
                    setRe(<UnsubBtn publisherId={props.user.userId} />)
                } else {
                    setRe(<SubBtn publisherId={props.user.userId} />)
                }
            })
            break;
        case 'mine':
            btn = <MineBtn redir={props.redir} articleId={props.articleId}></MineBtn>
            break;

    }
    return (
        <div className="d-flex flex-nowrap">
            <img src={`data:image/png;base64, ${props.user.avaBase}`} className="circleAvaMain" />
            <p className="fw-normal fs-4 text-nowrap ms-3" style={{ display: 'inline', lineHeight: '6rem' }}>{props.user.name}</p>

            <div className="w-100" style={{ lineHeight: '6rem', paddingTop: '2rem' }}>
                {btn}{re}
            </div>

        </div>
    )
}

export default UserComp