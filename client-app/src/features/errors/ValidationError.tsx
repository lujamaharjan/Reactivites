import {Message} from "semantic-ui-react";

interface Props
{
    errors: string[]
}

export default function ValidationError({errors}: Props) {
    return (
        <ul>
            <Message error>
                {errors &&(
                    <Message.List>
                        {errors.map((error, i) => (
                            <Message.Item key={i}>{error}</Message.Item>
                        ))}
                    </Message.List>
                )}
            </Message>
        </ul>
    )
}