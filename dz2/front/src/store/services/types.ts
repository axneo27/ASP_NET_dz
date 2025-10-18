export interface ServiceResponse {
    message: string;
    isSuccess: boolean;
    payload: unknown;
    statusCode: number;
}